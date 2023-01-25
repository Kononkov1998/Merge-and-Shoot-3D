using DG.Tweening;
using UnityEngine;

namespace Shields
{
    public class Shield : MonoBehaviour, IDamageReceiver
    {
        private const float OffsetWhenHit = 0.1f;
        private const float OffsetWhenHitDuration = 0.1f;
        private const float ActivationDuration = 1f;

        private Vector3 _activePosition;
        private Vector3 _inactivePosition;

        private void Awake() =>
            _activePosition = transform.position;

        public void Initialize(Vector3 inactivePosition) =>
            _inactivePosition = inactivePosition;

        public void TakeDamage(float _) =>
            DOTween.Sequence(transform)
                .Append(transform.DOMove(_activePosition + Vector3.left * OffsetWhenHit, OffsetWhenHitDuration))
                .Append(transform.DOMove(_activePosition, OffsetWhenHitDuration));

        public void Activate()
        {
            transform.DOKill();
            transform.DOMove(_activePosition, ActivationDuration);
        }

        public void Deactivate()
        {
            transform.DOKill();
            transform.DOMove(_inactivePosition, ActivationDuration);
        }
    }
}