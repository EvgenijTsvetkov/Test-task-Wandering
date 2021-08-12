using Entities;
using UnityEngine;

namespace Logic.States
{
    public class WalkerWalk : State
    {
        private readonly Walker entity;

        public WalkerWalk(StateMachine stateMachine, Walker entity) : base(stateMachine)
        {
            this.entity = entity;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (entity.IsMoving())
                return;

            stateMachine.ChangeState(entity.IdleState);
        }

        public void SetDestination(Vector3 destination) => 
            entity.Move.ToDestination(destination);
    }
}