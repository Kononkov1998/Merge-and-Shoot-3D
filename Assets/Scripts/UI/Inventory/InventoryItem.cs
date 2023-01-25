using Hero;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _level;

        private IItemCombiner _itemCombiner;
        private WeaponData _weaponData;
        private Vector3 _startPosition;
        private Transform _startParent;

        public InventorySlot Slot { get; private set; }
        public int Level => _weaponData.Level;

        public void Initialize(IItemCombiner inventory, InventorySlot slot, WeaponData weaponData)
        {
            _itemCombiner = inventory;
            _weaponData = weaponData;
            _image.sprite = _weaponData.Sprite;
            _level.text = _weaponData.Level.ToString();
            AttachToNewSlot(slot);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
            _startPosition = transform.position;
            _startParent = transform.parent;
            transform.SetParent(_itemCombiner.Parent);
        }

        public void OnDrag(PointerEventData eventData) =>
            transform.position = Input.mousePosition;

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            transform.position = _startPosition;
            transform.SetParent(_startParent);
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (!eventData.pointerDrag.TryGetComponent(out InventoryItem item)) return;
            if (_itemCombiner.CanCombine(item, this))
                _itemCombiner.CombineItems(item, this);
        }

        public void DettachSelf()
        {
            if (Slot != null)
                Slot.Dettach();
        }

        private void AttachToNewSlot(InventorySlot slot)
        {
            DettachSelf();
            Slot = slot;
            Slot.Attach();
        }
    }
}