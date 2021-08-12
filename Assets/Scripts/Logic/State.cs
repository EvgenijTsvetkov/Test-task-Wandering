using UnityEngine;

namespace Logic
{
    public abstract class State
    {
        protected readonly StateMachine stateMachine;
        protected float startTime;

        protected State(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnFixedUpdate()
        {
        }

        public virtual void OnStateEnter() =>
            startTime = Time.time;

        public virtual void OnStateExit()
        {
        }
    }
}