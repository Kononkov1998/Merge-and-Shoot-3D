using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private ShootTarget _shootTarget;
        [SerializeField] private DamageIndicator _damageIndicatorPrefab;

        public float Current { get; private set; }
        public float Max { get; private set; }

        public event Action Changed;

        public void Construct(float max)
        {
            Max = max;
            Current = max;
        }

        public void TakeDamage(float damage)
        {
            float clampedDamage = Mathf.Min(damage, Current);
            Current -= clampedDamage;
            DamageIndicator damageIndicator = Instantiate(
                _damageIndicatorPrefab,
                _shootTarget.transform.position,
                _damageIndicatorPrefab.transform.rotation
            );
            damageIndicator.Construct(damage);
            Changed?.Invoke();
        }
    }
}