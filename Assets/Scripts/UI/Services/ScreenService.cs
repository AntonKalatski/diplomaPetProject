using System;
using Factories.Interfaces;

namespace UI.Services
{
    public class ScreenService : IScreenService
    {
        private readonly IGameUIFactory uiFactory;

        public ScreenService(IGameUIFactory uiFactory)
        {
            this.uiFactory = uiFactory;
        }

        public void Open(ScreenType type)
        {
            switch (type)
            {
                case ScreenType.Unknown:
                    
                    break;
                case ScreenType.Shop:
                    uiFactory.CreateShop();
                    break;
                case ScreenType.MainMenu:

                    break;
                case ScreenType.Settings:

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}