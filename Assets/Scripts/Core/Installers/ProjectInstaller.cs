using Core.Services.AssetProvider;
using Core.Services.Factory;
using Core.Services.Input;
using Core.Services.Progress;
using Core.Services.Random;
using Core.Services.SaveLoad;
using Core.Services.SceneManagement;
using Core.Services.StateMachine;
using Core.Services.StaticData;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameBootstrap _gameBootstrapPrefab;

        public override void InstallBindings()
        {
            BindStaticDataService();
            BindRandomService();
            BindProgressService();
            BindSaveLoadService();
            BindSceneLoader();
            BingGameStateMachine();
            BindStatesFactory();
            BindInputService();
            BindGameFactory();
            BindAssetsProvider();
            BindGameBootstrap();
        }

        private void BindRandomService()
        {
            Container
                .Bind<IRandomService>()
                .To<RandomService>()
                .AsSingle();
        }

        private void BindGameBootstrap()
        {
            Container
                .BindInterfacesAndSelfTo<GameBootstrap>()
                .FromComponentInNewPrefab(_gameBootstrapPrefab)
                .AsSingle()
                .NonLazy();
        }

        private void BindStaticDataService()
        {
            Container
                .Bind<IStaticDataService>()
                .To<StaticDataService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindProgressService()
        {
            Container
                .Bind<IProgressService>()
                .To<ProgressService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSaveLoadService()
        {
            Container
                .Bind<ISaveLoadService>()
                .To<SaveLoadService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSceneLoader()
        {
            Container
                .Bind<ISceneLoader>()
                .To<SceneLoader>()
                .AsSingle();
        }

        private void BingGameStateMachine()
        {
            Container
                .Bind<IGameStateMachine>()
                .To<GameStateMachine>()
                .AsSingle()
                .NonLazy();
        }

        private void BindStatesFactory()
        {
            Container
                .Bind<IStatesFactory>()
                .To<StatesFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void BindInputService()
        {
            Container
                .Bind<IInputService>()
                .To<InputService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindGameFactory()
        {
            Container
                .Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void BindAssetsProvider()
        {
            Container
                .Bind<IAssetsProvider>()
                .To<AddressablesProvider>()
                .AsSingle()
                .NonLazy();
        }
    }
}