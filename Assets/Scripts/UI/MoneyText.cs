using Data.Progress;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MoneyText : MonoBehaviour, ISavedProgressReader
    {
        private MoneyProgress _money;
        private TextMeshProUGUI _text;

        private void Awake() => 
            _text = GetComponent<TextMeshProUGUI>();

        private void Start()
        {
            _money.Changed += UpdateMoneyText;
            UpdateMoneyText();
        }

        private void OnDestroy() => 
            _money.Changed -= UpdateMoneyText;

        public void LoadProgress(ProgressData progress) => 
            _money = progress.MoneyProgress;

        private void UpdateMoneyText() => 
            _text.text = $"Coins: {_money.Value}";
    }
}