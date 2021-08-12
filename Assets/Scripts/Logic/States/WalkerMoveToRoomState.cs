using Entities;
using Logic.InteractDoor;

namespace Logic.States
{
    public class WalkerMoveToRoomState : State
    {
        private readonly Walker entity;
        private readonly StateType stateType;
        private Door openedDoor;

        public WalkerMoveToRoomState(StateMachine stateMachine, Walker entity, StateType stateType) : base(stateMachine)
        {
            this.entity = entity;
            this.stateType = stateType;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (IsReached())
                stateMachine.ChangeState(entity.IdleState);
            else if (IsCloseDoor())
                TransitionNextState();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            entity.Move.ToDestination(entity.CurrentRoom.GetRandomPoint());
            entity.DisplayIconState(stateType);
        }

        public void SetOpenedDoor(Door door) =>
            openedDoor = door;

        private bool IsReached() =>
            !entity.Move.IsMoving();

        private bool IsCloseDoor() =>
            openedDoor.Status == DoorStatus.Close;


        private void TransitionNextState()
        {
            var foundRoom = entity.RoomsService.FindNearestRoom(entity.Transform.position, null);

            if (foundRoom != entity.CurrentRoom)
                entity.CurrentRoom = foundRoom;

            stateMachine.ChangeState(entity.IdleState);
        }
    }
}