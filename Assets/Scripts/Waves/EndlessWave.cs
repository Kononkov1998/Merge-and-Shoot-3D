using System.Collections;
using Core.Services.Factory;
using UnityEngine;
using Utilities.Extensions;

namespace Waves
{
    public class EndlessWave : BaseWave
    {
        public EndlessWave(ICoroutineRunner coroutineRunner, IGameFactory gameFactory)
            : base(coroutineRunner, gameFactory)
        {
        }

        protected override IEnumerator SpawningRoutine()
        {
            if (!ValidateCooldown())
                yield break;

            var spawnCooldown = new WaitForSeconds(Data.SpawnCooldown);

            while (true)
            {
                if (Enemies.Count >= Data.MaxEnemiesOnScreenInEndlessMode)
                {
                    yield return spawnCooldown;
                    continue;
                }

                SpawnEnemy(Data.BunchOfEnemies.RandomElement().EnemyType);
                yield return spawnCooldown;
            }
        }

        private bool ValidateCooldown()
        {
            if (Data.SpawnCooldown > 0.0001f)
                return true;

            Debug.LogError("Cooldown is too low. Possible endless loop");
            return false;
        }
    }
}