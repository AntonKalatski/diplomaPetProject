using Bootstrap;
using Configs;
using Configs.Player;
using Extensions;
using Factories.Interfaces;
using GameElements.Health;
using GameSM.Interfaces;
using Services.Configs.Zombie;
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

        public void Enter(string payload)
        {
            curtain.Show();
            prefabFactory.CleanUp();
            uiFactory.CleanUp();
            sceneLoader.Load(payload, onLoaded: OnLevelLoaded);
        }

        public void Exit() => curtain.Show();

        private void OnLevelLoaded()
        {
            InitializeGameWorld();
            InformProgressReaders();
            gameStateMachine.Enter<GameLoopState>();
        }

        private void InitializeGameWorld()
        {
            InitializeZombieSpawners();

            var survivor = InitializePlayer();
            
            ServiceLocator.Container.LocateService<CameraService>().SetFollower(survivor.transform);
            InitializeHud(survivor.GetComponent<IHealth>());
        }

        private void InitializeZombieSpawners()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelData levelData = configsService.ForLevel(sceneKey);
            foreach (var spawner in levelData.zombieSpawners)
                prefabFactory.CreateZombieSpawner(spawner.Position, spawner.Id, spawner.zombieType);
        }

        private GameObject InitializePlayer()
        {
            InitializeSpawnPoint();
            var spawnPoint = gameProgressService.PlayerProgressData.worldData.PositionOnLevel.position.AsUnityVector3();
            GameObject survivor = prefabFactory.CreateSurvivor(spawnPoint);
            return survivor;
        }

        private void InitializeHud(IHealth playerHealth) =>
            uiFactory.CreateHud().GetComponent<ActorUI>().Initialize(playerHealth);

        private void InitializeSpawnPoint()
        {
            var playerConfig = configsService.ForConfig<PlayerConfig>(ConfigType.PlayerConfig);

            if (!gameProgressService.IsNewGame)
                return;
            gameProgressService.PlayerProgressData.worldData.PositionOnLevel.position =
                playerConfig.playerSpawnPoint.position.AsVectorPosition();
        }

        private void InformProgressReaders()
        {
            prefabFactory.ProgressLoadables.ForEach(x => x.LoadProgress(gameProgressService.PlayerProgressData));
            uiFactory.ProgressLoadables.ForEach(x => x.LoadProgress(gameProgressService.PlayerProgressData));
        }
    }
}