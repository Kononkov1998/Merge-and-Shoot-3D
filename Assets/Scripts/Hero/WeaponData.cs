using UnityEngine;

namespace Hero
{
    [CreateAssetMenu(fileName = "Weapon")]
    public class WeaponData : ScriptableObject
    {
        public int Level;
        public float Damage;
        public float FireRate;
        public float FireDistance;
        [Range(0, 100f)] public float CriticalChance;
        public float CriticalMultiplier;
        public Sprite Sprite;
    }
}