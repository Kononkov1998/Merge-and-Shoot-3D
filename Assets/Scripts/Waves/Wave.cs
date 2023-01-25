using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Services.Factory;
using Core.Services.Random;
using Data;
using Data.Enemy;
using UnityEngine;

namespace Waves
{
    public class Wave : BaseWave
    {
        private readonly IRandomService _random;
        private Dictionary<EnemyType, int> _enemiesLeft;
        private int _enemiesToSpawn;
        private EnemyType _lastSpawnedEnemyType;

        public Wave(ICoroutineRunner coroutineRunner, IGameFactory gameFactory, IRandomService random)
            : base(coroutineRunner, gameFactory)
        {
            _random = random;
        }

        public override void StartSpawning()
        {
            _enemiesToSpawn = Data.BunchOfEnemies.Sum(x => x.Count);
            _enemiesLeft = Data.BunchOfEnemies.ToDictionary(
                x => x.EnemyType,
                x => x.Count
            );
            base.StartSpawning();
        }

        protected override IEnumerator SpawningRoutine()
        {
            var spawnCooldown = new WaitForSeconds(Data.SpawnCooldown);

            while (_enemiesToSpawn > 0)
            {
                _lastSpawnedEnemyType = RandomEnemyType();
                SpawnEnemy(_lastSpawnedEnemyType);
                _enemiesToSpawn--;
                yield return spawnCooldown;
            }
        }

        private EnemyType RandomEnemyType()
        {
            int randomIndex = _random.Next(0, _enemiesLeft.Count);
            return _enemiesLeft.ElementAt(randomIndex).Key;
        }

        protected override void OnEnemyDied()
        {
            base.OnEnemyDied();

            _enemiesLeft[_lastSpawnedEnemyType]--;
            if (_enemiesLeft[_lastSpawnedEnemyType] != 0) return;

            _enemiesLeft.Remove(_lastSpawnedEnemyType);
            if (_enemiesLeft.Count == 0)
                InvokeCompleted();
        }
    }
}