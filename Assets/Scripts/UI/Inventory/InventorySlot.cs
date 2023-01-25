using UnityEngine;

namespace UI.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        public bool Free { get; private set; } = true;

        public void Attach() =>
            Free = false;

        public void Dettach() =>
            Free = true;
    }
}