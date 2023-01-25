using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Enemies
{
    public class DamageIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _damageText;
        [SerializeField] private float _lifetime;

        private void Start()
        {
            transform.DOMoveY(100f, 0.25f)
                .SetSpeedBased(true);

            DOTween.Sequence()
                .Append(_damageText.DOFade(0f, _lifetime))
                .OnComplete(Destroy);
        }

        public void Construct(float damage) =>
            _damageText.text = damage.ToString();

        private void Destroy()
        {
            transform.DOKill();
            Destroy(gameObject);
        }
    }
}