using Constants;
using Factories.Interfaces;
using Providers.Assets;
using Services.Configs.Zombie;
using Services.GameProgress;
using Services.GameServiceLocator;
using UI.Bars;
using UI.Elements;
using UI.Services;
using UnityEngine;

namespace Factories
{
    public class GameUIFactory : AbstractGameFactory, IGameUIFactory
    {
        private const string UIRootPath = "UI/UIRoot/UIRoot";
        private readonly IGameProgressService progressService;
        private readonly IConfigsService configsService;
        private readonly ServiceLocator serviceLocator;
        private Transform uiRoot;

        public GameUIFactory(IGameProgressService progressService, IAssetProvider assetProvider,
            IConfigsService configsService, ServiceLocator serviceLocator) : base(assetProvider)
        {
            this.progressService = progressService;
            this.configsService = configsService;
            this.serviceLocator = serviceLocator;
        }

        public void CreateUIRoot() => uiRoot = Instantiate(AssetsPath.UIRoot).transform;

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetsPath.Hud);
            hud.GetComponentInChildren<KillCounterBar>().Construct(progressService.PlayerProgressData);
            foreach (var button in hud.GetComponentsInChildren<OpenScreenButton>())
                button.Construct(serviceLocator.LocateService<IScreenService>());

            return hud;
        }

        public void CreateShop()
        {
            var windowConfig = configsService.ForScreen(ScreenType.Shop);
            Object.Instantiate(windowConfig.prefab, uiRoot).Construct(progressService);
        }
    }
}