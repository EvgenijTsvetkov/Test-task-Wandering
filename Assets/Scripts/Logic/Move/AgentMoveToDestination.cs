using UnityEngine;
using UnityEngine.AI;

namespace Logic.Move
{
    public class AgentMoveToDestination : Move
    {
        [SerializeField] private NavMeshAgent agent;

        public bool IsMoving() =>
            agent.remainingDistance > agent.stoppingDistance;

        public bool IsDestination(Vector3 destination) => 
            agent.SetDestination(destination) && agent.CalculatePath(destination, agent.path);

        public void ToDestination(Vector3 destination) => 
            agent.destination = destination;
    }
}