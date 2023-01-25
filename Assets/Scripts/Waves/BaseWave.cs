using System;
using System.Collections;
using System.Collections.Generic;
using Core.Services.Factory;
using Data;
using Data.Enemy;
using Enemies;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Waves
{
    public abstract class BaseWave : IWave
    {
        protected WaveData Data;
        private GameObject _target;
        private Coroutine _spawningRoutine;

        private readonly IGameFactory _gameFactory;
        private readonly ICoroutineRunner _coroutineRunner;

        public Queue<EnemyRoot> Enemies { get; } = new();

        public event Action EnemySpawned;
        public event Action EnemyDied;
        public event Action Completed;

        protected BaseWave(ICoroutineRunner coroutineRunner, IGameFactory gameFactory)
        {
            _coroutineRunner = coroutineRunner;
            _gameFactory = gameFactory;
        }

        public void Initialize(WaveData data, GameObject targetForEnemies)
        {
            Data = data;
            _target = targetForEnemies;
        }

        public virtual void StartSpawning() => 
            _spawningRoutine = _coroutineRunner.StartCoroutine(SpawningRoutine());

        public void CleanUp()
        {
            _coroutineRunner.StopCoroutine(_spawningRoutine);
            while (Enemies.Count > 0)
                Object.Destroy(Enemies.Dequeue().gameObject);
        }

        protected abstract IEnumerator SpawningRoutine();

        protected virtual void OnEnemyDied()
        {
            EnemyRoot enemy = Enemies.Dequeue();
            enemy.Death.Happened -= OnEnemyDied;
            EnemyDied?.Invoke();
        }

        protected void SpawnEnemy(EnemyType enemyType)
        {
            EnemyRoot monster = _gameFactory.CreateMonster(enemyType, _target);
            monster.Death.Happened += OnEnemyDied;
            Enemies.Enqueue(monster);
            EnemySpawned?.Invoke();
        }

        protected void InvokeCompleted() => 
            Completed?.Invoke();
    }
}