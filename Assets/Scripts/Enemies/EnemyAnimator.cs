using UnityEngine;

namespace Enemies
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int DieHash = Animator.StringToHash("Die");

        public void PlayAttack() => 
            _animator.SetTrigger(AttackHash);

        public void Die() => 
            _animator.SetTrigger(DieHash);
    }
}