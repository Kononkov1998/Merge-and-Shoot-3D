using Core.Services.StaticData;
using Data.Progress;
using Hero;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WeaponInfoText : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private TextMeshProUGUI _text;
        private IStaticDataService _staticData;
        private WeaponProgress _weaponProgress;

        private void Start()
        {
            _weaponProgress.Changed += UpdateWeaponInfoText;
            UpdateWeaponInfoText();
        }

        private void OnDestroy() => 
            _weaponProgress.Changed += UpdateWeaponInfoText;


        public void Construct(IStaticDataService staticData) => 
            _staticData = staticData;

        public void LoadProgress(ProgressData progress) => 
            _weaponProgress = progress.WeaponProgress;

        private void UpdateWeaponInfoText()
        {
            WeaponData data = _staticData.ForWeapon(_weaponProgress.Level);
            _text.text = "WEAPON INFO\n" +
                         $"Level {data.Level}\n" +
                         $"Damage {data.Damage}\n" +
                         $"FireRate {data.FireRate}\n" +
                         $"FireDistance {data.FireDistance}\n" +
                         $"CriticalChance {data.CriticalChance}\n" +
                         $"CriticalMultiplier {data.CriticalMultiplier}";
        }
    }
}