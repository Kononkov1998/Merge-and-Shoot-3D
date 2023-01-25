using UnityEngine;

namespace Hero
{
    public class HeroAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationClip _attackClip;

        private static readonly int AttackHash = Animator.StringToHash("IsAttacking");
        private static readonly int DieHash = Animator.StringToHash("Die");

        public void PlayAttack() =>
            _animator.SetBool(AttackHash, true);

        public void StopAttack() =>
            _animator.SetBool(AttackHash, false);

        public void Die() =>
            _animator.SetTrigger(DieHash);

        public float AttackDuration() =>
            _attackClip.length;

        public void SetSpeed(float speed) =>
            _animator.speed = speed;
    }
}