using System;
using System.Collections.Generic;
using Data;
using Enemies;
using UnityEngine;

namespace Waves
{
    public interface IWave
    {
        void Initialize(WaveData forWave, GameObject targetForEnemies);
        event Action EnemySpawned;
        event Action EnemyDied;
        event Action Completed;
        void StartSpawning();
        Queue<EnemyRoot> Enemies { get; }
        void CleanUp();
    }
}