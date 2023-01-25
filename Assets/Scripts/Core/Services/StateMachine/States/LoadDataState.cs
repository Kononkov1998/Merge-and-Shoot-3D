using System.Threading.Tasks;
using Core.Services.Progress;
using Core.Services.SaveLoad;
using Core.Services.StaticData;
using Core.StateMachine;
using Data.Progress;

namespace Core.Services.StateMachine.States
{
    public class LoadDataState : IState
    {
        private const string GameplaySceneName = "Main";

        private readonly IProgressService _progress;
        private readonly ISaveLoadService _saveLoad;
        private readonly IStaticDataService _staticData;
        private readonly IGameStateMachine _gameStateMachine;

        public LoadDataState(IGameStateMachine gameStateMachine, IProgressService progress,
            ISaveLoadService saveLoad, IStaticDataService staticData)
        {
            _gameStateMachine = gameStateMachine;
            _progress = progress;
            _saveLoad = saveLoad;
            _staticData = staticData;
        }

        public async void Enter()
        {
            await LoadStaticData();
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, string>(GameplaySceneName);
        }

        private Task LoadStaticData() => 
            _staticData.Load();

        private void LoadProgressOrInitNew()
        {
            _progress.Progress =
                _saveLoad.LoadProgress()
                ?? new ProgressData();
        }

        public void Exit()
        {
        }
    }
}