﻿using System.Collections.Generic;
using Bootstrap;
using Extensions;
using Factories.Interfaces;
using GameElements.Health;
using GameSM.Interfaces;
using Player;
using Providers;
using Services.GameCamera;
using Services.GameProgress;
using Services.GameServiceLocator;
using UI.Actors;
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
        private LevelConfigProvider levelConfig;

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
            GetCurrentLevelConfig();
            InitializeGameWorld();
            InformProgressReaders();
            gameStateMachine.Enter<GameLoopState>();
        }

        private void GetCurrentLevelConfig() => levelConfig = Object.FindObjectOfType<LevelConfigProvider>();

        private void InitializeGameWorld()
        {
            var survivor = InitializePlayer();
            ServiceLocator.Container.LocateService<CameraService>().SetFollower(survivor.transform);
            InitializeHud(survivor.GetComponent<IHealth>());
        }

        private void InitializeHud(IHealth playerHealth) =>
            uiFactory.CreateHud().GetComponent<ActorUI>().Initialize(playerHealth);

        private GameObject InitializePlayer()
        {
            InitializeSpawnPoint();
            var spawnPoint = gameProgressService.PlayerProgressData.worldData.PositionOnLevel.position.AsUnityVector3();
            GameObject survivor = prefabFactory.CreateSurvivor(spawnPoint);
            return survivor;
        }

        private void InitializeSpawnPoint()
        {
            if (!gameProgressService.IsNewGame)
                return;
            gameProgressService.PlayerProgressData.worldData.PositionOnLevel.position =
                levelConfig.GetSpawnPoint().transform.position.AsVectorPosition();
        }

        private void InformProgressReaders()
        {
            foreach (IGameFactory factory in factories)
                factory.ProgressLoadables.ForEach(x => x.LoadProgress(gameProgressService.PlayerProgressData));
        }
    }
}