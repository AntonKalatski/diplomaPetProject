using System;
using System.Threading.Tasks;
using Bootstrap;
using Extensions;
using Factories.Interfaces;
using GameElements.Health;
using GameSM.Interfaces;
using Providers.Assets;
using Services.Configs;
using Services.GameCamera;
using Services.GameProgress;
using Services.GameServiceLocator;
using UI.Actors;
using UI.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSM.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly IGamePrefabFactory prefabFactory;
        private readonly IGameProgressService gameProgressService;
        private readonly IGameUIFactory uiFactory;
        private readonly IConfigsService configsService;
        private readonly SceneLoader sceneLoader;
        private readonly LoadingCurtain curtain;

        public LoadLevelState(
            GameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain curtain,
            IGameUIFactory uiFactory,
            IGamePrefabFactory prefabFactory,
            IGameProgressService gameProgressService,
            IConfigsService configsService)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.curtain = curtain;
            this.uiFactory = uiFactory;
            this.prefabFactory = prefabFactory;
            this.gameProgressService = gameProgressService;
            this.configsService = configsService;
        }

        public async void Enter(string payload)
        {
            Debug.Log("Enter LoadLevelState");
            curtain.Show();
            prefabFactory.CleanUp();
            uiFactory.CleanUp();
            //assetProvider.CleanUp();

            // await prefabFactory.WarmUp();
            // await uiFactory.WarmUp();
            await sceneLoader.Load(payload, onLoaded: OnLevelLoaded);
        }

        public void Exit()
        {
            Debug.Log("Exit LoadLevelState");
        }

        private async void OnLevelLoaded()
        {
            await InitializeGameWorld();
            InformProgressReaders();
            gameStateMachine.Enter<GameLoopState>();
        }

        private async Task InitializeGameWorld()
        {
            var levelData = LevelData();

            InitializeZombieSpawners(levelData);
            var survivor = await InitializePlayer(levelData);

            InitializeUiRoot();
            await InitializeHud(survivor.GetComponent<IHealth>());

            ServiceLocator.Container.LocateService<CameraService>().SetFollower(survivor.transform);
        }

        private async Task<GameObject> InitializePlayer(LevelData levelData)
        {
            var spawnPoint = InitializeSpawnPoint(levelData);
            GameObject survivor = await prefabFactory.CreateSurvivor(spawnPoint);
            return survivor;
        }


        private void InitializeZombieSpawners(LevelData levelData)
        {
            foreach (var spawner in levelData.zombieSpawners)
                prefabFactory.CreateZombieSpawner(spawner.Position, spawner.Id, spawner.zombieType);
        }

        private void InitializeUiRoot() => uiFactory.CreateUIRoot();

        private async Task InitializeHud(IHealth playerHealth)
        {
            var hud = await uiFactory.CreateHud();
            hud.GetComponent<ActorUI>().Initialize(playerHealth);
        }

        private Vector3 InitializeSpawnPoint(LevelData levelData)
        {
            if (gameProgressService.IsNewGame)
            {
                gameProgressService.PlayerProgressData.worldData.PositionOnLevel.position =
                    levelData.InitialHeroPosition.AsVectorPosition();
            }

            return gameProgressService.PlayerProgressData.worldData.PositionOnLevel.position.AsUnityVector3();
        }

        private void InformProgressReaders()
        {
            prefabFactory.ProgressLoadables.ForEach(x => x.LoadProgress(gameProgressService.PlayerProgressData));
            uiFactory.ProgressLoadables.ForEach(x => x.LoadProgress(gameProgressService.PlayerProgressData));
        }

        private LevelData LevelData()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelData levelData = configsService.ForLevel(sceneKey);
            return levelData;
        }
    }
}