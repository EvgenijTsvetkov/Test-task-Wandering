using System.Collections.Generic;
using Logic;
using Logic.Move;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private AgentMoveToDestination move;

        protected StateMachine stateMachine;
        protected IDictionary<StateType, State> states;

        public AgentMoveToDestination Move => move;
        public Transform Transform => transform;

        public bool IsMoving() => move.IsMoving();
        
        public virtual void Awake() =>
            CreateStateMachine();
        
        private void Update() =>
            stateMachine.CurrentState?.OnUpdate();

        private void FixedUpdate() =>
            stateMachine.CurrentState?.OnFixedUpdate();

        protected abstract void CreateStateMachine();
    }
}