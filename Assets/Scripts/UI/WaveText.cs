using Data.Progress;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WaveText : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private TextMeshProUGUI _text;
        private WaveProgress _wave;

        private void OnDestroy() => 
            _wave.Changed -= UpdateWaveText;

        public void LoadProgress(ProgressData progress)
        {
            _wave = progress.WaveProgress;
            _wave.Changed += UpdateWaveText;
            UpdateWaveText();
        }

        private void UpdateWaveText() => 
            _text.text = $"WAVE {_wave.Number}";
    }
}