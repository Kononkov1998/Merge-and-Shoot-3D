namespace Core.StateMachine
{
    public interface IState : IExitState
    {
        void Enter();
    }
}