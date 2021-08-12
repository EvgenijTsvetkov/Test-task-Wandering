using System.Collections.Generic;
using Logic;
using Logic.InteractDoor;
using Logic.States;
using Services;
using Zenject;

namespace Entities
{
    public class Walker : Entity
    {
        private RoomsService roomsService;
        private Room currentRoom;
        
        public WalkerIdle IdleState => (WalkerIdle) states[StateType.Idle];
        public WalkerWalk WalkState => (WalkerWalk) states[StateType.Walk];
        public WalkerMoveToRoomState MoveToRoomState => (WalkerMoveToRoomState) states[StateType.MoveToRoom];
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
                {StateType.Idle, new WalkerIdle(stateMachine, this)},
                {StateType.Walk, new WalkerWalk(stateMachine, this)},
                {StateType.MoveToRoom, new WalkerMoveToRoomState(stateMachine, this)},
            };
        }

        protected internal void MoveToRoom(Door openedDoor)
        {
            MoveToRoomState.SetOpenedDoor(openedDoor);
            stateMachine.ChangeState(MoveToRoomState);
        }
    }
}