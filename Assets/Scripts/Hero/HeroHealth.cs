using System;
using UnityEngine;

namespace Hero
{
    public class HeroHealth : MonoBehaviour, IHealth
    {
        [field: SerializeField] public float Current { get; private set; } = 5;
        [field: SerializeField] public float Max { get; private set; } = 5;

        public void TakeDamage(float damage)
        {
            float clampedDamage = Mathf.Min(damage, Current);
            Current -= clampedDamage;
            Changed?.Invoke();
        }

        public event Action Changed;
    }
}