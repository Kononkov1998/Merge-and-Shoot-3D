using UnityEngine;

namespace UI.Inventory
{
    public interface IItemCombiner
    {
        bool CanCombine(InventoryItem item1, InventoryItem item2);
        void CombineItems(InventoryItem draggedItem, InventoryItem targetItem);
        Transform Parent { get; }
    }
}