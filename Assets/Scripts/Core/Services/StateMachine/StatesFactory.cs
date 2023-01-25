using Core.StateMachine;
using Zenject;

namespace Core.Services.StateMachine
{
    public class StatesFactory : IStatesFactory
    {
        private readonly DiContainer _diContainer;

        public StatesFactory(DiContainer diContainer) =>
            _diContainer = diContainer;

        public TState Create<TStateMachine, TState>(TStateMachine stateMachine)
            where TStateMachine : class, IStateMachine
            where TState : class, IExitState =>
            _diContainer.Instantiate<TState>(new[] {stateMachine});

        public TState Create<TState>() where TState : class, IExitState =>
            _diContainer.Instantiate<TState>();
    }
}