using System;
using UnityEngine;

namespace Data.Progress
{
    [Serializable]
    public class MoneyProgress
    {
        [field: SerializeField] public int Value { get; private set; }

        public event Action Changed;

        public void Add(int value)
        {
            if (value <= 0)
                Debug.LogError("Money to add needs to be more than 0");

            Value += value;
            Changed?.Invoke();
        }

        public void Withdraw(int value)
        {
            if (value <= 0)
                Debug.LogError("Money to withdraw needs to be more than 0");

            Value -= value;
            Changed?.Invoke();
        }
    }
}