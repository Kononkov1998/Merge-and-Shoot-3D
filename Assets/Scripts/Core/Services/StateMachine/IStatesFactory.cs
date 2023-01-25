using Core.StateMachine;

namespace Core.Services.StateMachine
{
    public interface IStatesFactory
    {
        TState Create<TStateMachine, TState>(TStateMachine stateMachine)
            where TStateMachine : class, IStateMachine
            where TState : class, IExitState;

        TState Create<TState>()
            where TState : class, IExitState;
    }
}