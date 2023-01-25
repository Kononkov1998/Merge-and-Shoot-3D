using Data;
using Data.Enemy;
using UnityEngine;

namespace Enemies
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;

        private IDamageReceiver _target;
        private float _maxCooldown;
        private float _cooldown;
        private float _damage;
        private bool _isAttacking;
        private bool _isActive;

        public void Construct(EnemyData data, IDamageReceiver target)
        {
            _target = target;
            _maxCooldown = data.AttackCooldown;
            _damage = data.Damage;
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        public void DisableAttack() =>
            _isActive = false;

        public void EnableAttack() =>
            _isActive = true;

        // used by animation event
        private void OnAttack() => 
            _target.TakeDamage(_damage);

        // used by animation event
        private void OnAttackEnded()
        {
            _cooldown = _maxCooldown;
            _isAttacking = false;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _cooldown -= Time.deltaTime;
        }

        private bool CooldownIsUp() =>
            _cooldown <= 0f;

        private bool CanAttack() =>
            _isActive && !_isAttacking && CooldownIsUp();

        private void StartAttack()
        {
            _animator.PlayAttack();
            _isAttacking = true;
        }
    }
}