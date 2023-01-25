using System;
using Core.Services.Random;
using Data;
using Data.Enemy;
using Data.Progress;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class EnemyDeath : MonoBehaviour, IDeath
    {
        [SerializeField] private EnemyMovement _movement;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private EnemyHealth _health;

        private IRandomService _random;
        private MoneyProgress _moneyProgress;
        private int _minMoney;
        private int _maxMoney;

        public event Action Happened;

        private void Start() =>
            _health.Changed += OnHealthChanged;

        private void OnDestroy() =>
            _health.Changed -= OnHealthChanged;

        public void Construct(EnemyData data, MoneyProgress moneyProgress, IRandomService random)
        {
            _minMoney = data.MinMoney;
            _maxMoney = data.MaxMoney;
            _moneyProgress = moneyProgress;
            _random = random;
        }

        private void OnHealthChanged()
        {
            if (_health.Current <= 0f)
                Die();
        }

        private void Die()
        {
            _moneyProgress.Add(_random.Next(_minMoney, _maxMoney));
            _movement.Stop();
            _animator.Die();
            Happened?.Invoke();

            DelayedDestroy();
        }

        private void DelayedDestroy()
        {
            DOTween.Sequence()
                .AppendInterval(1f)
                .OnComplete(() => transform
                    .DOMove(Vector3.down * 100f, .15f)
                    .SetSpeedBased(true)
                );

            DOTween.Sequence()
                .AppendInterval(3f)
                .OnComplete(Destroy);
        }

        private void Destroy()
        {
            transform.DOKill();
            Destroy(gameObject);
        }
    }
}