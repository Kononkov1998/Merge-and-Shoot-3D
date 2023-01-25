using Data;
using Data.Enemy;
using UnityEngine;
using Utilities.Extensions;

namespace Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private EnemyAttack _attack;
        
        private float _attackDistance;
        private float _movementSpeed;
        private Transform _heroTransform;
        private bool _enabled;

        public void Construct(EnemyData data, Transform heroTransform)
        {
            _heroTransform = heroTransform;
            _attackDistance = data.AttackDistance;
            _movementSpeed = data.MovementSpeed;
            transform.LookAt(_heroTransform);
            _enabled = true;
        }

        private void Update()
        {
            if (!_enabled) return;

            if (HeroNotReached())
                MoveToHero();
            else
                StartAttacking();
        }

        public void Stop() => 
            _enabled = false;

        private bool HeroNotReached() =>
            transform.position.SqrMagnitudeTo(_heroTransform.position) >= _attackDistance;

        private void MoveToHero() =>
            transform.position = Vector3.MoveTowards(
                transform.position,
                _heroTransform.position,
                _movementSpeed * Time.deltaTime
            );

        private void StartAttacking()
        {
            _enabled = false;
            _attack.EnableAttack();
        }
    }
}