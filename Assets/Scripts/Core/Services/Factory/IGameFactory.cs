using Data.Enemy;
using Enemies;
using Hero;
using Shields;
using UI;
using UI.Inventory;
using UnityEngine;
using Waves;

namespace Core.Services.Factory
{
    public interface IGameFactory : IService
    {
        HeroRoot CreateHero();
        Hud CreateHud();
        Shield CreateShield();
        InventoryItem CreateItem(IItemCombiner itemCombiner, InventorySlot slot, int level);
        IWave CreateWave(GameObject targetForEnemies, bool endless);
        EnemyRoot CreateMonster(EnemyType enemyType, GameObject target);
    }
}