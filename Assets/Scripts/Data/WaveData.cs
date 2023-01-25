using Data.Enemy;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "WaveData")]
    public class WaveData : ScriptableObject
    {
        public int Number;
        public float SpawnCooldown;
        public int MaxEnemiesOnScreenInEndlessMode;
        public BunchOfEnemies[] BunchOfEnemies;
    }
}