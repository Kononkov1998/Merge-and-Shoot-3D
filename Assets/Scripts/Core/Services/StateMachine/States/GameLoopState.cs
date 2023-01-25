using Core.Services.Factory;
using Core.Services.Input;
using Core.Services.Progress;
using Core.Services.SaveLoad;
using Core.Services.SceneManagement;
using Core.StateMachine;
using Data.PayLoads;
using DG.Tweening;
using Enemies;
using Hero;
using Shields;
using UI;
using UnityEngine;
using Waves;

namespace Core.Services.StateMachine.States
{
    public class GameLoopState : IPayloadState<GameWorldPayload>
    {
        private readonly IGameFactory _gameFactory;
        private readonly ISaveLoadService _saveLoad;
        private readonly IInputService _input;
        private readonly ISceneLoader _sceneLoader;
        private readonly IProgressService _progress;

        private HeroRoot _hero;
        private IWave _wave;
        private FightButton _fightButton;
        private GameObject _hud;
        private Shield _shield;

        public GameLoopState(IGameFactory gameFactory, ISaveLoadService saveLoad,
            IInputService input, ISceneLoader sceneLoader, IProgressService progress)
        {
            _gameFactory = gameFactory;
            _saveLoad = saveLoad;
            _input = input;
            _sceneLoader = sceneLoader;
            _progress = progress;
        }

        public void Enter(GameWorldPayload payload)
        {
            _hero = payload.Hero;
            _fightButton = payload.Hud.FightButton;
            _shield = payload.Shield;

            _input.FightButtonPressed += StartFight;
            _hero.Death.Happened += RestartGame;

            StartEndlessWave();
            UpdateHeroTarget();
        }

        public void Exit()
        {
            _input.FightButtonPressed -= StartFight;
            _hero.Death.Happened -= RestartGame;
            DisposeWave();
        }

        private void StartFight()
        {
            DisposeWave();
            StartCompletableWave();
            UpdateHeroTarget();
        }

        private void RestartGame()
        {
            DOTween.Sequence()
                .AppendInterval(3f)
                .OnComplete(() => _sceneLoader.ReloadCurrentScene());
        }

        private void StartEndlessWave()
        {
            _wave = _gameFactory.CreateWave(_shield.gameObject, true);
            SubscribeToWave();
            _wave.StartSpawning();

            _shield.Activate();
            _fightButton.Show();
        }

        private void StartCompletableWave()
        {
            _wave = _gameFactory.CreateWave(_hero.gameObject, false);
            SubscribeToWave();
            _wave.StartSpawning();

            _shield.Deactivate();
        }

        private void SubscribeToWave()
        {
            _wave.EnemySpawned += OnEnemySpawned;
            _wave.EnemyDied += OnEnemyDied;
            _wave.Completed += OnWaveCompleted;
        }

        private void OnWaveCompleted()
        {
            _progress.Progress.WaveProgress.IncreaseNumber();
            DisposeWave();
            StartEndlessWave();
            UpdateHeroTarget();
            _saveLoad.SaveProgress();
        }

        private void DisposeWave()
        {
            _wave.EnemySpawned -= OnEnemySpawned;
            _wave.EnemyDied -= OnEnemyDied;
            _wave.Completed -= OnWaveCompleted;
            _wave.CleanUp();
        }

        private void OnEnemySpawned()
        {
            if (_hero.Attack.IsActive) return;

            _hero.Attack.Enable();
            UpdateHeroTarget();
        }

        private void OnEnemyDied()
        {
            if (_wave.Enemies.Count > 0)
                UpdateHeroTarget();
            else
                _hero.Attack.Disable();
        }

        private void UpdateHeroTarget()
        {
            EnemyRoot enemy = _wave.Enemies.Peek();
            _hero.Attack.SelectTarget(enemy);
            _hero.Aim.SelectTarget(enemy.ShootTarget.transform);
        }
    }
}