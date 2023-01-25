using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Services.AssetProvider;
using Data;
using Data.Enemy;
using Data.Shield;
using Hero;
using Shields;
using UI;
using UI.Inventory;
using UnityEngine;
using Utilities.Extensions;

namespace Core.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyType, EnemyData> _enemiesData;
        private Dictionary<int, WaveData> _wavesData;
        private Dictionary<int, WeaponData> _weaponsData;
        private SceneData _sceneData;
        private readonly IAssetsProvider _assetsProvider;

        public StaticDataService(IAssetsProvider assetsProvider) =>
            _assetsProvider = assetsProvider;

        public Hud HudPrefab { get; private set; }
        public InventoryItem InventoryItemPrefab { get; private set; }
        public HeroData HeroData { get; private set; }
        public ShieldData ShieldData { get; private set; }

        public Vector3 CameraPosition => _sceneData.CameraData.Position;

        public async Task Load()
        {
            await Task.WhenAll(
                LoadEnemiesData(),
                LoadSceneData(),
                LoadWavesData(),
                LoadWeaponsData(),
                LoadInventoryData(),
                LoadHeroData(),
                LoadHudPrefab(),
                LoadShieldData()
            );
        }

        public EnemyData ForEnemy(EnemyType enemyType) =>
            _enemiesData[enemyType];

        public WaveData ForWave(int waveNumber) =>
            _wavesData[waveNumber];

        public WeaponData ForWeapon(int weaponLevel)
        {
            return _weaponsData.ContainsKey(weaponLevel)
                ? _weaponsData[weaponLevel]
                : null;
        }

        public Vector3 RandomEnemySpawnPosition()
        {
            Vector3 firstPosition = _sceneData.EnemyFirstSpawnData.Position;
            Vector3 secondPosition = _sceneData.EnemySecondSpawnData.Position;
            return firstPosition.RandomPointOnLine(secondPosition);
        }

        private async Task LoadSceneData() =>
            _sceneData = await _assetsProvider.LoadAsset<SceneData>("SceneData");

        private async Task LoadEnemiesData()
        {
            IList<EnemyData> result = await _assetsProvider.LoadAssets<EnemyData>("Enemy");
            _enemiesData = result.ToDictionary(x => x.EnemyType, x => x);
        }

        private async Task LoadWavesData()
        {
            IList<WaveData> result = await _assetsProvider.LoadAssets<WaveData>("Wave");
            _wavesData = result.ToDictionary(x => x.Number, x => x);
        }

        private async Task LoadWeaponsData()
        {
            IList<WeaponData> result = await _assetsProvider.LoadAssets<WeaponData>("Weapon");
            _weaponsData = result.ToDictionary(x => x.Level, x => x);
        }

        private async Task LoadInventoryData() =>
            InventoryItemPrefab = await _assetsProvider.LoadAsset<InventoryItem>("InventoryItem");

        private async Task LoadHeroData() =>
            HeroData = new HeroData
            {
                Prefab = await _assetsProvider.LoadAsset<HeroRoot>("Hero"),
                TransformData = _sceneData.HeroSpawnData
            };

        private async Task LoadShieldData() =>
            ShieldData = new ShieldData
            {
                Prefab = await _assetsProvider.LoadAsset<Shield>("Shield"),
                ShieldSpawnData = _sceneData.ShieldSpawnData
            };

        private async Task LoadHudPrefab() =>
            HudPrefab = await _assetsProvider.LoadAsset<Hud>("HUD");
    }
}