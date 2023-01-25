using System;

namespace Core.Services.Input
{
    public class InputService : IInputService
    {
        public event Action FightButtonPressed;
        public event Action BuyWeaponButtonPressed;

        public void InvokeFightButtonPressed() => 
            FightButtonPressed?.Invoke();

        public void InvokeBuyWeaponButtonPressed() => 
            BuyWeaponButtonPressed?.Invoke();
    }
}