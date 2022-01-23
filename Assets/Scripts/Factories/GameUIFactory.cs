using System.Threading.Tasks;
using Constants;
using Factories.Interfaces;
using Providers.Assets;
using Services.Ads;
using Services.Configs.Zombie;
using Services.GameProgress;
using UI.Bars;
using UI.Elements;
using UI.Screens.Shop;
using UI.Services;
using UnityEngine;

namespace Factories
{
    public class GameUIFactory : AbstractGameFactory, IGameUIFactory
    {
        // private const string UIRootPath = "UI/UIRoot/UIRoot";
        private readonly IGameProgressService progressService;
        private readonly IConfigsService configsService;
        private readonly IScreenService screenService;

        private readonly IAdsService adsService;

        private Transform uiRoot;

        public GameUIFactory(IGameProgressService progressService, IAssetProvider assetProvider,
            IConfigsService configsService, IScreenService screenService, IAdsService adsService) : base(assetProvider)
        {
            this.progressService = progressService;
            this.configsService = configsService;
            this.screenService = screenService;
            this.adsService = adsService;
        }

        public async Task WarmUp()
        {
            await assetProvider.Load<GameObject>(AssetsAdresses.UIRoot);
            await assetProvider.Load<GameObject>(AssetsAdresses.Hud);
        }

        public async void CreateUIRoot()
        {
            var prefab = await Instantiate(AssetsAdresses.UIRoot);
            uiRoot = prefab.transform;
        }

        public async Task<GameObject> CreateHud()
        {
            GameObject hud = await InstantiateRegisteredAsync(AssetsAdresses.Hud);
            hud.GetComponentInChildren<KillCounterBar>().Construct(progressService.PlayerProgressData);
            foreach (var button in hud.GetComponentsInChildren<OpenScreenButton>())
                button.Construct(screenService);

            return hud;
        }

        public void CreateShop()
        {
            // var windowConfig = configsService.ForScreen(ScreenType.Shop);
            // ShopScreen shopWindow = Object.Instantiate(windowConfig.prefab, uiRoot) as ShopScreen;
            // shopWindow.Construct(adsService, progressService, inAppService, assetProvider);
        }
    }
}