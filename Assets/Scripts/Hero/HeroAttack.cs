using Core.Services.Random;
using Core.Services.StaticData;
using Data.Progress;
using Enemies;
using UnityEngine;

namespace Hero
{
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private ShotTrail _shotTrailPrefab;
        [SerializeField] private Transform _shotStartPoint;

        private IStaticDataService _staticData;
        private IRandomService _random;
        private WeaponProgress _weaponProgress;
        private WeaponData _weapon;
        private EnemyRoot _target;
        private bool _isAttacking;

        public bool IsActive { get; private set; }

        private void Update()
        {
            if (CanAttack() && !_isAttacking)
                StartAttack();
            else if (!CanAttack() && _isAttacking)
                StopAttack();
        }

        private void OnDestroy() =>
            _weaponProgress.Changed -= UpdateWeapon;

        public void Construct(IStaticDataService staticData, IRandomService random)
        {
            _staticData = staticData;
            _random = random;
        }

        public void SelectTarget(EnemyRoot target) =>
            _target = target;

        public void Enable() =>
            IsActive = true;

        public void Disable()
        {
            IsActive = false;
            StopAttack();
        }

        public void LoadProgress(ProgressData progress)
        {
            _weaponProgress = progress.WeaponProgress;
            _weaponProgress.Changed += UpdateWeapon;
            UpdateWeapon();
        }

        private void UpdateWeapon()
        {
            _weapon = _staticData.ForWeapon(_weaponProgress.Level);
            UpdateAnimationSpeed();
        }

        // used by animation event
        private void OnAttack()
        {
            if (_target == null) return;
            ShotTrail shotTrail = Instantiate(_shotTrailPrefab, _shotStartPoint.position, Quaternion.identity);
            shotTrail.Construct(_target.ShootTarget.transform.position);
            _target.Health.TakeDamage(CalculateDamage());
        }

        // used by animation event
        private void OnAttackEnded()
        {
        }

        private float CalculateDamage()
        {
            float random = _random.Next(0, 100f);
            if (random <= _weapon.CriticalChance)
                return _weapon.Damage * _weapon.CriticalMultiplier;
            return _weapon.Damage;
        }

        private bool CanAttack() =>
            IsActive && _target != null && _target.Health.Current > 0f && TargetInRange();

        private bool TargetInRange() =>
            _target.transform.position.x - transform.position.x <= _weapon.FireDistance;

        private void StartAttack()
        {
            _isAttacking = true;
            UpdateAnimationSpeed();
            _animator.PlayAttack();
        }

        private void UpdateAnimationSpeed() => 
            _animator.SetSpeed(_weapon.FireRate / _animator.AttackDuration());

        private void StopAttack()
        {
            _isAttacking = false;
            _animator.SetSpeed(1);
            _animator.StopAttack();
        }
    }
}