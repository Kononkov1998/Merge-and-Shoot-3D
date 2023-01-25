using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Hero
{
    public class HeroDeath : MonoBehaviour, IDeath
    {
        [SerializeField] private RigBuilder _rigBuilder;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private HeroHealth _health;

        public event Action Happened;

        private void Start() => 
            _health.Changed += OnHealthChanged;

        private void OnDestroy() => 
            _health.Changed -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (_health.Current <= 0f)
                Die();
        }

        private void Die()
        {
            _health.Changed -= OnHealthChanged;
            _rigBuilder.Clear();
            _attack.Disable();
            _animator.Die();
            Happened?.Invoke();
        }
    }
}