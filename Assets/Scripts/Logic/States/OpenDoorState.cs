using Entities;
using Logic.InteractDoor;

namespace Logic.States
{
    public class OpenDoorState : State
    {
        private readonly WalkerInteractDoor entity;
        private Door foundDoor;
        public OpenDoorState(StateMachine stateMachine, WalkerInteractDoor entity) : base(stateMachine)
        {
            this.entity = entity;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (IsNotReachedDoor())
                return;

            if (IsCanOpenDoor())
                foundDoor.Open();
            else if (IsCanChangeState())
                MoveInFoundDoor();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            foundDoor = entity.InteractDoorsService.FindNearestDoor(entity.Transform.position);

            if (foundDoor is null)
                stateMachine.ChangeState(entity.IdleState);
            else
                entity.Move.ToDestination(foundDoor.Position());
        }

        public override void OnStateExit()
        {
            base.OnStateExit();

            foundDoor = null;
            entity.StartCoroutine(entity.WaitAndChangeStateToOpenDoor());
        }

        private bool IsNotReachedDoor() =>
            foundDoor is null || entity.Move.IsMoving();

        private bool IsCanOpenDoor() =>
            foundDoor.Status == DoorStatus.Close;

        private bool IsCanChangeState() =>
            foundDoor.Status == DoorStatus.Open && !entity.Move.IsMoving();

        private void MoveInFoundDoor()
        {
            var foundRoom = entity.RoomsService.FindNearestRoom(entity.Transform.position, entity.CurrentRoom);
            
            entity.InvokeOpenDoor(foundDoor, foundRoom);
            entity.CurrentRoom = foundRoom;
            entity.MoveToRoom(foundDoor);
        }
    }
}