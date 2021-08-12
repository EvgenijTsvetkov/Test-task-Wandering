using System.Collections.Generic;
using Logic;
using Logic.InteractDoor;
using Logic.States;
using Misc;
using Services;
using UnityEngine;
using Zenject;

namespace Entities
{
    public class Walker : Entity
    {
        [SerializeField] private DisplayStatusState displayStatusState;
        
        private RoomsService roomsService;
        private Room currentRoom;
        
        public WalkerIdle IdleState => (WalkerIdle) states[StateType.Idle];
        public WalkerWalk WalkState => (WalkerWalk) states[StateType.Walk];
        private WalkerMoveToRoomState MoveToRoomState => (WalkerMoveToRoomState) states[StateType.MoveToRoom];
        protected internal RoomsService RoomsService => roomsService;

        public Room CurrentRoom
        {
            get => currentRoom;
            set => currentRoom = value;
        }

        [Inject]
        private void Construct(RoomsService roomsService, WalkersService walkersService)
        {
            this.roomsService = roomsService;
            walkersService.RegisterWalker(this);
        }

        public override void Awake()
        {
            base.Awake();

            currentRoom = roomsService.FindNearestRoom(Transform.position, null);
        }

        public virtual void Start() =>
            stateMachine.ChangeState(IdleState);

        protected override void CreateStateMachine()
        {
            stateMachine = new StateMachine();

            states = new Dictionary<StateType, State>
            {
                {StateType.Idle, new WalkerIdle(stateMachine, this, StateType.Idle)},
                {StateType.Walk, new WalkerWalk(stateMachine, this, StateType.Walk)},
                {StateType.MoveToRoom, new WalkerMoveToRoomState(stateMachine, this, StateType.MoveToRoom)},
            };
        }

        protected internal void MoveToRoom(Door openedDoor)
        {
            MoveToRoomState.SetOpenedDoor(openedDoor);
            stateMachine.ChangeState(MoveToRoomState);
        }

        protected internal void DisplayIconState(StateType stateType) =>
            displayStatusState.Display(stateType);
    }
}