using Entities;
using UnityEngine;

namespace Logic.States
{
    public class WalkerIdle : State
    {
        private const float MinDuration = .5f;
        private const float MaxDuration = 4;

        private readonly Walker entity;
        private readonly StateType stateType;
        private float duration;

   
        protected internal WalkerIdle(StateMachine stateMachine, Walker entity, StateType stateType) : base(stateMachine)
        {
            this.entity = entity;
            this.stateType = stateType;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (IsTimeNotOver())
                return;

            var pointInRoom = entity.CurrentRoom.GetRandomPoint();
            if (IsNotDestination(pointInRoom)) return;
            
            entity.WalkState.SetDestination(pointInRoom);
            stateMachine.ChangeState(entity.WalkState);
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            RandomizeDuration();
            entity.DisplayIconState(stateType);
        }

        private void RandomizeDuration() =>
            duration = Random.Range(MinDuration, MaxDuration);

        private bool IsNotDestination(Vector3 pointOnSquare) => 
            !entity.Move.IsDestination(pointOnSquare);

        private bool IsTimeNotOver() =>
            Time.time < startTime + duration;
    }
}