using System.Threading.Tasks;
using Data;
using Data.Enemy;
using Data.Shield;
using Hero;
using UI;
using UI.Inventory;
using UnityEngine;

namespace Core.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        Task Load();
        EnemyData ForEnemy(EnemyType enemyType);
        Vector3 RandomEnemySpawnPosition();
        WaveData ForWave(int waveNumber);
        WeaponData ForWeapon(int weaponLevel);
        public Vector3 CameraPosition { get; }
        Hud HudPrefab { get; }
        InventoryItem InventoryItemPrefab { get; }
        HeroData HeroData { get; }
        ShieldData ShieldData { get; }
    }
}