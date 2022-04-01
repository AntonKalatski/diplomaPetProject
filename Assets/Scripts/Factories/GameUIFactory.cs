using System.Threading.Tasks;
using Constants;
using Factories.Interfaces;
using GameSM;
using Providers.Assets;
using Services;
using Services.Ads;
using Services.Configs;
using Services.GameProgress;
using Services.GameSound;
using Services.GameVibration;
using UI.Bars;
using UI.Elements;
using UI.Screens.MainMenuScreen;
using UI.Services;
using UnityEngine;

namespace Factories
{
    public class GameUIFactory : AbstractGameFactory, IGameUIFactory
    {
        // private const string UIRootPath = "UI/UIRoot/UIRoot";
        private readonly GameStateMachine gameStateMachine;
        private readonly IGameProgressService progressService;
        private readonly IConfigsService configsService;
        private readonly IScreenService screenService;
        private readonly ISoundService soundService;
        private readonly IInputService inputService;
        private readonly IVibrationService vibrationService;
        private readonly IAdsService adsService;

        private Transform uiRoot;

        public GameUIFactory(
            GameStateMachine gameStateMachine,
            IGameProgressService progressService,
            IAssetProvider assetProvider,
            IInputService inputService,
            IConfigsService configsService,
            IScreenService screenService,
            IAdsService adsService) : base(assetProvider)
        {
            this.gameStateMachine = gameStateMachine;
            this.progressService = progressService;
            this.configsService = configsService;
            this.screenService = screenService;
            this.inputService = inputService;
            this.adsService = adsService;
        }

        public async Task WarmUp()
        {
            //await assetProvider.Load<GameObject>(AssetsAdresses.UIRoot);
            //await assetProvider.Load<GameObject>(AssetsAdresses.Hud);
        }


        public async void CreateUIRoot()
        {
            var prefab = await Instantiate(AssetsAdresses.UIRoot);
            uiRoot = prefab.transform;
        }

        public async Task<GameObject> CreateHud()
        {
            GameObject hud;
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                {
                    hud = await InstantiateRegisteredAsync(AssetsAdresses.HudStandalone);
                    break;
                }
                case RuntimePlatform.Android:
                default:
                {
                    hud = await InstantiateRegisteredAsync(AssetsAdresses.Hud);
                    inputService.AddInputPorvider(hud.GetComponentInChildren<AttackButton>());
                    break;
                }
            }

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

        public void CreateMainMenu()
        {
            var windowConfig = configsService.ForScreen(ScreenType.MainMenu);
            MainMenuScreen mainMenu = Object.Instantiate(windowConfig.prefab, uiRoot) as MainMenuScreen;
            mainMenu.Construct(gameStateMachine, progressService);
            foreach (var button in mainMenu.GetComponentsInChildren<OpenScreenButton>())
                button.Construct(screenService);
        }

        public void CreateSettings()
        {
            var windowConfig = configsService.ForScreen(ScreenType.Settings);
            SettingsScreen settingsScreen = Object.Instantiate(windowConfig.prefab, uiRoot) as SettingsScreen;
            settingsScreen.Construct(progressService, soundService, vibrationService);
        }
    }
}