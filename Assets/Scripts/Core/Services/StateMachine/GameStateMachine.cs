using Core.Services.StateMachine.States;
using Core.StateMachine;

namespace Core.Services.StateMachine
{
    public class GameStateMachine : BaseStateMachine, IGameStateMachine
    {
        private readonly IStatesFactory _statesFactory;

        public GameStateMachine(IStatesFactory statesFactory)
        {
            _statesFactory = statesFactory;
            RegisterStates();
        }

        private void RegisterStates()
        {
            RegisterState(_statesFactory.Create<IGameStateMachine, BootstrapState>(this));
            RegisterState(_statesFactory.Create<IGameStateMachine, LoadDataState>(this));
            RegisterState(_statesFactory.Create<IGameStateMachine, LoadLevelState>(this));
            RegisterState(_statesFactory.Create<GameLoopState>());
        }
    }
}