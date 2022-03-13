using System;
using Factories.Interfaces;
using Services.GameServiceLocator;

namespace UI.Services
{
    public class ScreenService : IScreenService
    {
        public void Open(ScreenType type)
        {
            switch (type)
            {
                case ScreenType.Unknown:
                    break;
                case ScreenType.Shop:
                    ServiceLocator.Container.LocateService<IGameUIFactory>().CreateShop();
                    break;
                case ScreenType.MainMenu:
                    ServiceLocator.Container.LocateService<IGameUIFactory>().CreateMainMenu();
                    break;
                case ScreenType.Settings:
                    ServiceLocator.Container.LocateService<IGameUIFactory>().CreateSettings();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}