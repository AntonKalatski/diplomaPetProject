using Bootstrap;
using Constants;
using Factories;
using GameSM.Interfaces;
using Providers.Assets;
using Services;
using Services.GameCamera;
using Services.GameInput;
using Services.GameProgress;
using Services.GameServiceLocator;

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

        private void EnterLoadLevel() => stateMachine.Enter<LoadLevelState, string>(GameConstants.Main);

        private void RegisterServices()
        {
            serviceLocator.RegisterService<IAssetProvider>(new AssetProvider());
            serviceLocator.RegisterService<CameraService>(new CameraService());
            serviceLocator.RegisterService<IInputService>(InputService());
            serviceLocator.RegisterService<IGameProgressService>(new GameProgressService());
            var assetProvider = serviceLocator.LocateService<IAssetProvider>();
            serviceLocator.RegisterService<IGamePrefabFactory>(new GamePrefabFactory(assetProvider));
            serviceLocator.RegisterService<IGameUIFactory>(new GameUIFactory(assetProvider));
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