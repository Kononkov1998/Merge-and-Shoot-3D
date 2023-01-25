using Core.Services.SceneManagement;
using Core.StateMachine;

namespace Core.Services.StateMachine.States
{
    public class BootstrapState : IState
    {
        private const string BootSceneName = "Boot";

        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            if (_sceneLoader.SceneLoaded(BootSceneName))
                EnterLoadDataState();
            else
                _sceneLoader.LoadScene(BootSceneName, EnterLoadDataState);
        }

        public void Exit()
        {
        }

        private void EnterLoadDataState() =>
            _gameStateMachine.Enter<LoadDataState>();
    }
}