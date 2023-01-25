using System;
using UnityEngine;

namespace Data.Progress
{
    [Serializable]
    public class WaveProgress
    {
        [field: SerializeField] public int Number { get; private set; } = 1;

        public event Action Changed;

        public void IncreaseNumber()
        {
            Number++;
            Changed?.Invoke();
        }
    }
}