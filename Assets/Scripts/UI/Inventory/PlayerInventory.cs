using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services.Factory;
using Core.Services.Input;
using Core.Services.SaveLoad;
using Core.Services.StaticData;
using Data.Progress;
using UnityEngine;

namespace UI.Inventory
{
    public class PlayerInventory : MonoBehaviour, IItemCombiner, ISavedProgressReader
    {
        [SerializeField] private InventorySlot[] _slots;

        private IInputService _input;
        private IGameFactory _factory;
        private IStaticDataService _staticData;
        private ISaveLoadService _saveLoad;
        private ProgressData _progress;
        public Transform Parent => transform.parent;

        public event Action ItemsCombined;

        private void Start()
        {
            _input.BuyWeaponButtonPressed += BuyWeapon;
            InitItems();
        }

        private void OnDestroy() =>
            _input.BuyWeaponButtonPressed -= BuyWeapon;

        public void Construct(IInputService input, IGameFactory factory,
            ISaveLoadService saveLoad, IStaticDataService staticData)
        {
            _input = input;
            _factory = factory;
            _saveLoad = saveLoad;
            _staticData = staticData;
        }

        public bool HasFreeSlots() =>
            _progress.InventoryItems.Count < _slots.Length;

        public void LoadProgress(ProgressData progress) =>
            _progress = progress;

        public bool CanCombine(InventoryItem item1, InventoryItem item2)
        {
            if (item1.Level != item2.Level) return false;
            return _staticData.ForWeapon(item1.Level + 1) != null;
        }

        public void CombineItems(InventoryItem draggedItem, InventoryItem targetItem)
        {
            int newItemLevel = draggedItem.Level + 1;
            InventorySlot slot = targetItem.Slot;

            DestroyItems(draggedItem, targetItem);
            UpdateProgress(newItemLevel);
            CreateItem(newItemLevel, slot);

            if (newItemLevel > _progress.WeaponProgress.Level)
                _progress.WeaponProgress.IncreaseLevel();

            ItemsCombined?.Invoke();
            _saveLoad.SaveProgress();
        }

        private void UpdateProgress(int newItemLevel)
        {
            _progress.InventoryItems.Remove(newItemLevel - 1);
            _progress.InventoryItems.Remove(newItemLevel - 1);
            _progress.InventoryItems.Add(newItemLevel);
        }

        private static void DestroyItems(params InventoryItem[] items)
        {
            foreach (InventoryItem item in items)
            {
                item.DettachSelf();
                Destroy(item.gameObject);
            }
        }

        private void InitItems()
        {
            foreach (int weaponLevel in _progress.InventoryItems)
                CreateItem(weaponLevel);
        }

        private void BuyWeapon()
        {
            _progress.MoneyProgress.Withdraw(50);
            _progress.InventoryItems.Add(1);
            CreateItem(1);

            _saveLoad.SaveProgress();
        }

        private void CreateItem(int weaponLevel)
        {
            InventorySlot freeSlot = _slots.First(x => x.Free);
            CreateItem(weaponLevel, freeSlot);
        }

        private void CreateItem(int weaponLevel, InventorySlot slot) =>
            _factory.CreateItem(this, slot, weaponLevel);
    }
}