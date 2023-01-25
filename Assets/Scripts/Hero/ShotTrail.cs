using DG.Tweening;
using UnityEngine;

namespace Hero
{
    public class ShotTrail : MonoBehaviour
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField] private float _lifetime;

        private void Start()
        {
            transform.DOMoveY(100f, 0.25f)
                .SetSpeedBased(true);
            ShrinkAndDestroy(ShrinkTween());
        }

        public void Construct(Vector3 to)
        {
            _line.positionCount = 2;
            _line.SetPosition(0, transform.position);
            _line.SetPosition(1, to);
        }

        private void ShrinkAndDestroy(Tween shrinkTween)
        {
            DOTween.Sequence()
                .AppendInterval(_lifetime)
                .Append(shrinkTween)
                .SetEase(Ease.InCubic)
                .OnComplete(Destroy);
        }

        private Tween ShrinkTween()
        {
            return DOTween.To(() => _line.startWidth,
                x =>
                {
                    _line.startWidth = x;
                    _line.endWidth = x;
                }, 0f, _lifetime);
        }

        private void Destroy()
        {
            transform.DOKill();
            Destroy(gameObject);
        }
    }
}