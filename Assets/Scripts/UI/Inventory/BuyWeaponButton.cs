using Core.Services.Input;
using Data.Progress;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class BuyWeaponButton : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private PlayerInventory _inventory;
        [SerializeField] private Button _button;
        private IInputService _input;
        private MoneyProgress _money;

        private void Start()
        {
            _inventory.ItemsCombined += UpdateInteraction;
            _button.onClick.AddListener(OnButtonClicked);
            UpdateInteraction();
        }

        private void OnDestroy()
        {
            _inventory.ItemsCombined -= UpdateInteraction;
            _button.onClick.RemoveListener(OnButtonClicked);
            _money.Changed -= UpdateInteraction;
        }

        public void Construct(IInputService input) =>
            _input = input;

        public void LoadProgress(ProgressData progress)
        {
            _money = progress.MoneyProgress;
            _money.Changed += UpdateInteraction;
        }

        private void OnButtonClicked()
        {
            _input.InvokeBuyWeaponButtonPressed();
            UpdateInteraction();
        }

        private void UpdateInteraction() =>
            _button.interactable = _money.Value >= 50 && _inventory.HasFreeSlots();
    }
}