namespace Logic
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }

        public void ChangeState(State newState)
        {
            CurrentState?.OnStateExit();
            CurrentState = newState;
            CurrentState?.OnStateEnter();
        }
    }
}