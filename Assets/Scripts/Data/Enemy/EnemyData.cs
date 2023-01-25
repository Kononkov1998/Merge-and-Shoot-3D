using Enemies;
using UnityEngine;

namespace Data.Enemy
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/New Enemy")]
    public class EnemyData : ScriptableObject
    {
        public EnemyRoot Prefab;
        public EnemyType EnemyType;
        public float Health;
        public float Damage;
        public float MovementSpeed;
        public float AttackDistance;
        public float AttackCooldown;
        public int MinMoney;
        public int MaxMoney;
    }
}