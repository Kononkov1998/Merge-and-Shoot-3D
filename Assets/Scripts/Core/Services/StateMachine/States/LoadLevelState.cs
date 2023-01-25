using Core.Services.Factory;
using Core.Services.Progress;
using Core.Services.SceneManagement;
using Core.StateMachine;
using Data.PayLoads;

namespace Core.Services.StateMachine.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly IProgressService _progress;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IGameStateMachine _stateMachine;

        public LoadLevelState(IGameStateMachine stateMachine, IProgressService progress,
            ISceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _progress = progress;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _progress.Cleanup();
            _sceneLoader.LoadScene(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            GameWorldPayload gameWorldPayload = InitGameWorld();
            _progress.LoadReadersProgress();
            _stateMachine.Enter<GameLoopState, GameWorldPayload>(gameWorldPayload);
        }

        public void Exit()
        {
        }

        private GameWorldPayload InitGameWorld()
        {
            return new GameWorldPayload
            {
                Hero = _gameFactory.CreateHero(),
                Hud = _gameFactory.CreateHud(),
                Shield = _gameFactory.CreateShield()
            };
        }
    }
}