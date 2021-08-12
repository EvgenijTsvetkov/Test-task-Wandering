using System;
using System.Collections;
using Logic;
using Logic.InteractDoor;
using Logic.States;
using Services;
using UnityEngine;
using Zenject;

namespace Entities
{
    public class WalkerInteractDoor : Walker
    {
        private const float CooldownForChangeState = 10f;

        private InteractDoorsService interactDoorsService;
        protected internal InteractDoorsService InteractDoorsService => interactDoorsService;

        protected internal event Action<Door, Room, Room> OpenDoor;

        private OpenDoorState OpenDoorState => (OpenDoorState) states[StateType.OpenDoor];

        [Inject]
        private void Construct(InteractDoorsService interactDoorsService)
        {
            this.interactDoorsService = interactDoorsService;
        }

        public override void Start()
        {
            base.Start();

            StartCoroutine(WaitAndChangeStateToOpenDoor());
        }

        protected override void CreateStateMachine()
        {
            base.CreateStateMachine();

            states.Add(StateType.OpenDoor, new OpenDoorState(stateMachine, this, StateType.OpenDoor));
        }

        protected internal void InvokeOpenDoor(Door openedDoor, Room to)
        {
            OpenDoor?.Invoke(openedDoor, CurrentRoom, to);
        }

        public IEnumerator WaitAndChangeStateToOpenDoor()
        {
            yield return new WaitForSeconds(CooldownForChangeState);

            stateMachine.ChangeState(OpenDoorState);
        }
    }
}