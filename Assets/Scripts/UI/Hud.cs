using UI.Inventory;
using UnityEngine;

namespace UI
{
    public class Hud : MonoBehaviour
    {
        [field: SerializeField] public FightButton FightButton { get; private set; }
        [field: SerializeField] public BuyWeaponButton BuyWeaponButton { get; private set; }
        [field: SerializeField] public WeaponInfoText WeaponInfoText { get; private set; }
        [field: SerializeField] public PlayerInventory Inventory { get; private set; }
    }
}