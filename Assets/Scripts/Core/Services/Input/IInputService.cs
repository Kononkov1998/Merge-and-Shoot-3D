using System;

namespace Core.Services.Input
{
    public interface IInputService : IService
    {
        void InvokeFightButtonPressed();
        void InvokeBuyWeaponButtonPressed();
        event Action FightButtonPressed;
        event Action BuyWeaponButtonPressed;
    }
}