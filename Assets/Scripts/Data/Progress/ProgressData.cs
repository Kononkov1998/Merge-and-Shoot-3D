using System;
using System.Collections.Generic;

namespace Data.Progress
{
    [Serializable]
    public class ProgressData
    {
        public WaveProgress WaveProgress;
        public WeaponProgress WeaponProgress;
        public MoneyProgress MoneyProgress;
        public List<int> InventoryItems;

        public ProgressData()
        {
            WaveProgress = new WaveProgress();
            WeaponProgress = new WeaponProgress();
            MoneyProgress = new MoneyProgress();
            InventoryItems = new List<int> {1};
        }
    }
}