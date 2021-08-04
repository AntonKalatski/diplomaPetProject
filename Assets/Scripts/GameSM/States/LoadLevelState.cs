using Bootstrap;
using Factories;
using GameSM.Interfaces;
using Providers;
using Services.GameCamera;
using Services.GameServiceLocator;
using UI.Loading;
using UnityEngine;

namespace GameSM.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly IGamePrefabFactory survivorFactory;
        private readonly IGameUIFactory uiFactory;
        private readonly SceneLoader sceneLoader;
        private readonly LoadingCurtain curtain;

        public LoadLevelState(
            GameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain curtain,
            IGameUIFactory uiFactory,
            IGamePrefabFactory survivorFactory)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.curtain = curtain;
            this.uiFactory = uiFactory;
            this.survivorFactory = survivorFactory;
        }

        public void Enter(string payload)
        {
            curtain.Show();
            sceneLoader.Load(payload, onLoaded: OnLevelLoaded);
        }

        private void OnLevelLoaded()
        {
            GameObject survivor = survivorFactory.CreateSurvivor(atPoint: Object.FindObjectOfType<LevelConfigProvider>().GetSpawnPoint());
            uiFactory.CreateHub();
            var cameraService = ServiceLocator.Container.LocateService<CameraService>();
            cameraService.SetFollower(survivor.transform);
            gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit() => curtain.Show();
    }
}