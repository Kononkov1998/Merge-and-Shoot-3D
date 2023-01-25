using Core.Services.Input;
using Core.Services.Progress;
using Core.Services.Random;
using Core.Services.SaveLoad;
using Core.Services.StaticData;
using Data.Enemy;
using Enemies;
using Hero;
using Shields;
using UI;
using UI.Inventory;
using UnityEngine;
using Waves;
using Zenject;

namespace Core.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticDataService _staticData;
        private readonly IProgressService _progress;
        private readonly DiContainer _diContainer;
        private readonly IInputService _input;
        private readonly ISaveLoadService _saveLoad;
        private readonly IRandomService _random;
        private HeroRoot _hero;
        private Camera _mainCamera;

        public GameFactory(IStaticDataService staticData, IProgressService progress,
            IInputService input, ISaveLoadService saveLoad, IRandomService random,
            DiContainer diContainer)
        {
            _staticData = staticData;
            _progress = progress;
            _input = input;
            _saveLoad = saveLoad;
            _random = random;
            _diContainer = diContainer;
        }

        public HeroRoot CreateHero()
        {
            HeroRoot hero = Instantiate(
                _staticData.HeroData.Prefab,
                _staticData.HeroData.TransformData.Position,
                _staticData.HeroData.TransformData.Rotation,
                null
            );

            hero.HealthBar.Initialize(_staticData.CameraPosition);
            hero.Attack.Construct(_staticData, _random);

            return hero;
        }

        public EnemyRoot CreateMonster(EnemyType enemyType, GameObject target)
        {
            EnemyData enemyData = _staticData.ForEnemy(enemyType);

            EnemyRoot enemy = Instantiate(
                enemyData.Prefab,
                _staticData.RandomEnemySpawnPosition(),
                Quaternion.identity,
                null
            );

            enemy.Health.Construct(enemyData.Health);
            enemy.Movement.Construct(enemyData, target.transform);
            enemy.Attack.Construct(enemyData, target.GetComponent<IDamageReceiver>());
            enemy.Death.Construct(enemyData, _progress.Progress.MoneyProgress, _random);
            enemy.HealthBar.Initialize(_staticData.CameraPosition);

            return enemy;
        }

        public Hud CreateHud()
        {
            Hud hud = Instantiate(_staticData.HudPrefab, null);
            hud.WeaponInfoText.Construct(_staticData);
            hud.FightButton.Construct(_input);
            hud.BuyWeaponButton.Construct(_input);
            hud.Inventory.Construct(_input, this, _saveLoad, _staticData);
            return hud;
        }

        public Shield CreateShield()
        {
            Shield shield = Instantiate(
                _staticData.ShieldData.Prefab,
                _staticData.ShieldData.ShieldSpawnData.Transform.Position,
                _staticData.ShieldData.ShieldSpawnData.Transform.Rotation,
                null
            );
            shield.Initialize(_staticData.ShieldData.ShieldSpawnData.InactivePosition);
            return shield;
        }

        public InventoryItem CreateItem(IItemCombiner itemCombiner, InventorySlot slot, int level)
        {
            InventoryItem item = Instantiate(_staticData.InventoryItemPrefab, slot.transform);
            item.Initialize(itemCombiner, slot, _staticData.ForWeapon(level));
            return item;
        }

        public IWave CreateWave(GameObject targetForEnemies, bool endless)
        {
            IWave wave = endless
                ? Instantiate<EndlessWave>()
                : Instantiate<Wave>();
            wave.Initialize(_staticData.ForWave(_progress.Progress.WaveProgress.Number), targetForEnemies);
            return wave;
        }

        private T Instantiate<T>()
        {
            var obj = _diContainer.Instantiate<T>();
            RegisterSavedProgress(obj);
            return obj;
        }

        private T Instantiate<T>(T prefab, Vector3 position,
            Quaternion rotation, Transform parent) where T : Component
        {
            T component = Object.Instantiate(prefab, position, rotation, parent);
            RegisterSavedProgress(component.gameObject);
            return component;
        }

        private T Instantiate<T>(T prefab, Transform parent) where T : Component
        {
            T component = Object.Instantiate(prefab, parent);
            RegisterSavedProgress(component.gameObject);
            return component;
        }

        private void RegisterSavedProgress(GameObject gameObject) =>
            _progress.RegisterSavedProgress(gameObject);

        private void RegisterSavedProgress<T>(T obj) =>
            _progress.RegisterSavedProgress(obj);
    }
}