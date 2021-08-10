using System.Collections.Generic;
using Bootstrap;
using Factories.Interfaces;
using GameSM.Interfaces;
using Providers;
using Services.GameCamera;
using Services.GameProgress;
using Services.GameServiceLocator;
using UI.Loading;
using UnityEngine;

namespace GameSM.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly IGamePrefabFactory prefabFactory;
        private readonly IGameProgressService gameProgressService;
        private readonly IGameUIFactory uiFactory;
        private readonly SceneLoader sceneLoader;
        private readonly LoadingCurtain curtain;
        private List<IGameFactory> factories;

        public LoadLevelState(
            GameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain curtain,
            IGameUIFactory uiFactory,
            IGamePrefabFactory prefabFactory,
            IGameProgressService gameProgressService)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.curtain = curtain;
            this.uiFactory = uiFactory;
            this.prefabFactory = prefabFactory;
            this.gameProgressService = gameProgressService;
        }

        public void Enter(string payload)
        {
            InitializeFactories();
            curtain.Show();
            prefabFactory.CleanUp();
            uiFactory.CleanUp();
            sceneLoader.Load(payload, onLoaded: OnLevelLoaded);
        }

        public void Exit() => curtain.Show();

        private void InitializeFactories()
        {
            factories = new List<IGameFactory>();
            factories.Add(prefabFactory);
            factories.Add(uiFactory);
        }

        private void OnLevelLoaded()
        {
            InitializeGameWorld();
            InformProgressReaders();
            gameStateMachine.Enter<GameLoopState>();
        }

        private void InitializeGameWorld()
        {
            GameObject survivor =
                prefabFactory.CreateSurvivor(atPoint: Object.FindObjectOfType<LevelConfigProvider>().GetSpawnPoint());
            uiFactory.CreateHub();
            var cameraService = ServiceLocator.Container.LocateService<CameraService>();
            cameraService.SetFollower(survivor.transform);
        }

        private void InformProgressReaders()
        {
            foreach (IGameFactory factory in factories)
            {
                factory.ProgressLoadables.ForEach(x => x.LoadProgress(gameProgressService.PlayerProgressData));
            }
        }
    }
}