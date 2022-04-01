using Bootstrap;
using Constants;
using Cysharp.Threading.Tasks;
using Factories;
using Factories.Interfaces;
using GameSM.Interfaces;
using Managers;
using Providers.Assets;
using Services;
using Services.Ads;
using Services.Configs;
using Services.GameCamera;
using Services.GameInput;
using Services.GameProgress;
using Services.GameServiceLocator;
using Services.Player;
using Services.Random;
using Services.SaveLoad;
using UI.Services;
using UnityEngine;

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

        public void Enter()
        {
            Debug.Log("Enter bootstrapstate");
            sceneLoader.Load(GameConstants.Loading, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
            Debug.Log("Exit bootstrapstate");
        }

        private void EnterLoadLevel() => stateMachine.Enter<LoadProgressState>();

        private void RegisterRandomService()
        {
            IRandomService randomService = new RandomService();
            serviceLocator.RegisterService(randomService);
        }

        private void RegisterServices()
        {
            RegisterConfigsService();
            RegisterRandomService();
            serviceLocator.RegisterService<IAssetProvider>(new AssetProvider());

            serviceLocator.RegisterService<IGameStateMachine>(stateMachine);
            serviceLocator.RegisterService<CameraService>(new CameraService());
            serviceLocator.RegisterService<IPlayerGameObjectProvider>(new PlayerGameObjectProvider());
            RegisterInputService();
            serviceLocator.RegisterService<IGameProgressService>(new GameProgressService());
            serviceLocator.RegisterService<IScreenService>(new ScreenService());
            //RegisterInAppService(new InAppProvider(), serviceLocator.LocateService<IGameProgressService>());

            RegisterGamePrefabFactory();
            RegisterGameUiFactory();


            serviceLocator.RegisterService<ISaveLoadService>(new SaveLoadService(
                serviceLocator.LocateService<IGameProgressService>(),
                serviceLocator.LocateService<IGamePrefabFactory>(), serviceLocator.LocateService<IGameUIFactory>()));
        }

        private void RegisterInputService()
        {
            var inputService = InputService();
            serviceLocator.RegisterService<IInputService>(inputService);
            serviceLocator.LocateService<ITickableManager>().AddTickable(inputService);
        }

        private void RegisterConfigsService()
        {
            IConfigsService configsService = new ConfigsService();
            configsService.LoadConfigs();
            serviceLocator.RegisterService(configsService);
        }

        private void RegisterAdsService()
        {
            IAdsService adsService = new AdsService();
            adsService.Initialize();
            serviceLocator.RegisterService(adsService);
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
                stateMachine,
                serviceLocator.LocateService<IGameProgressService>(),
                serviceLocator.LocateService<IAssetProvider>(),
                serviceLocator.LocateService<IConfigsService>(),
                serviceLocator.LocateService<IScreenService>(),
                serviceLocator.LocateService<IAdsService>()));
            serviceLocator.LocateService<ITestService>();
        }

        private IInputService InputService()
        {
#if UNITY_EDITOR
            return new StandaloneInputService();
#else
            return new MobileInputService();
#endif
        }
    }

    internal interface ITestService : IService
    {
    }
}

// private void RegisterInAppService(InAppProvider inAppProvider, IGameProgressService progressService)
// {
//     InAppService inAppService = new InAppService(inAppProvider, progressService);
//     inAppService.Initialize();
//     serviceLocator.RegisterService<IInAppService>(inAppService);
// }