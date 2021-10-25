using Bootstrap;
using Constants;
using Factories;
using Factories.Interfaces;
using GameSM.Interfaces;
using Providers.Assets;
using Services;
using Services.Ads;
using Services.Configs;
using Services.Configs.Zombie;
using Services.GameCamera;
using Services.GameInput;
using Services.GameProgress;
using Services.GameServiceLocator;
using Services.Player;
using Services.Random;
using Services.SaveLoad;
using UI.Services;

namespace GameSM.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine stateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly ServiceLocator serviceLocator;

        public BootstrapState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            ServiceLocator serviceLocator)
        {
            this.stateMachine = stateMachine;
            this.sceneLoader = sceneLoader;
            this.serviceLocator = serviceLocator;

            RegisterServices();
        }

        public void Enter() => sceneLoader.Load(GameConstants.Loading, onLoaded: EnterLoadLevel);

        public void Exit()
        {
        }

        private void EnterLoadLevel() => stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterConfigsService();
            RegisterRandomService();
            RegisterAdsService();
            serviceLocator.RegisterService<IAssetProvider>(new AssetProvider());
            serviceLocator.RegisterService<CameraService>(new CameraService());
            serviceLocator.RegisterService<IPlayerGameObjectProvider>(new PlayerGameObjectProvider());
            serviceLocator.RegisterService<IInputService>(InputService());
            serviceLocator.RegisterService<IGameProgressService>(new GameProgressService());

            RegisterGamePrefabFactory();
            RegisterGameUiFactory();

            serviceLocator.RegisterService<IScreenService>(
                new ScreenService(serviceLocator.LocateService<IGameUIFactory>()));


            serviceLocator.RegisterService<ISaveLoadService>(new SaveLoadService(
                serviceLocator.LocateService<IGameProgressService>(),
                serviceLocator.LocateService<IGamePrefabFactory>(), serviceLocator.LocateService<IGameUIFactory>()));
        }

        private void RegisterAdsService()
        {
            var adsService = new AdsService();
            adsService.Initialize();
            serviceLocator.RegisterService<IAdsService>(adsService);
        }

        private void RegisterConfigsService()
        {
            IConfigsService configsService = new ConfigsService();
            configsService.LoadConfigs();
            serviceLocator.RegisterService(configsService);
        }

        private void RegisterRandomService()
        {
            IRandomService randomService = new RandomService();
            serviceLocator.RegisterService(randomService);
        }

        private void RegisterGamePrefabFactory()
        {
            serviceLocator.RegisterService<IGamePrefabFactory>(
                new GamePrefabFactory(serviceLocator.LocateService<IAssetProvider>(),
                    serviceLocator.LocateService<IPlayerGameObjectProvider>(),
                    serviceLocator.LocateService<IConfigsService>(),
                    serviceLocator.LocateService<IRandomService>(),
                    serviceLocator.LocateService<IGameProgressService>()));
        }

        private void RegisterGameUiFactory()
        {
            serviceLocator.RegisterService<IGameUIFactory>(new GameUIFactory(
                serviceLocator.LocateService<IGameProgressService>(),
                serviceLocator.LocateService<IAssetProvider>(),
                serviceLocator.LocateService<IConfigsService>(), serviceLocator));
        }

        private IInputService InputService()
        {
#if UNITY_EDITOR
            return new MobileInputService();
#else
            return new StandaloneInputService();
#endif
        }
    }
}