using System;
using UnityEngine;

namespace Data.Progress
{
    [Serializable]
    public class WeaponProgress
    {
        [field: SerializeField] public int Level { get; private set; } = 1;

        public event Action Changed;

        public void IncreaseLevel()
        {
            Level++;
            Changed?.Invoke();
        }
    }
}