using Entities;
using UnityEngine;

namespace Logic.States
{
    public class WalkerWalk : State
    {
        private readonly Walker entity;
        private readonly StateType stateType;

        public WalkerWalk(StateMachine stateMachine, Walker entity, StateType stateType) : base(stateMachine)
        {
            this.entity = entity;
            this.stateType = stateType;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (entity.IsMoving())
                return;

            stateMachine.ChangeState(entity.IdleState);
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            
            entity.DisplayIconState(stateType);
        }

        public void SetDestination(Vector3 destination) => 
            entity.Move.ToDestination(destination);
    }
}