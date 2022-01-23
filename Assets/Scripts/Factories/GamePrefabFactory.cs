using System;
using System.Threading.Tasks;
using Behaviours.Loot;
using Configs.LootConfig;
using Configs.Zombie;
using Constants;
using Cysharp.Threading.Tasks;
using Factories.Interfaces;
using GameElements.Health;
using Player;
using Providers.Assets;
using Services;
using Services.Configs.Zombie;
using Services.GameCamera;
using Services.GameProgress;
using Services.GameServiceLocator;
using Services.Player;
using Services.Random;
using Spawner;
using UI.Actors;
using UnityEngine;
using Zombies;
using Object = UnityEngine.Object;

namespace Factories
{
    public class GamePrefabFactory : AbstractGameFactory, IGamePrefabFactory
    {
        private readonly IPlayerGameObjectProvider playerGOService;
        private readonly IGameProgressService gameProgressService;
        private readonly IConfigsService configsService;
        private readonly IRandomService randomService;

        public GamePrefabFactory(
            IAssetProvider assetProvider,
            IPlayerGameObjectProvider playerGOService,
            IConfigsService configsService,
            IRandomService randomService,
            IGameProgressService gameProgressService) : base(assetProvider)
        {
            this.playerGOService = playerGOService;
            this.configsService = configsService;
            this.randomService = randomService;
            this.gameProgressService = gameProgressService;
        }

        public async Task<GameObject> CreateSurvivor(Vector3 atPoint)
        {
            var prefab = await assetProvider.Load<GameObject>(AssetsAdresses.FemaleSurvivor);
            var player = InstantiateRegisteredAsync(prefab, atPoint);
            var inputService = ServiceLocator.Container.LocateService<IInputService>();
            var cameraService = ServiceLocator.Container.LocateService<CameraService>();
            player.GetComponent<PlayerMovement>().Construct(cameraService, inputService);
            playerGOService.SetPlayerGameObject(player);
            return player;
        }

        public async Task WarmUp()
        {
            await assetProvider.Load<GameObject>(AssetsAdresses.FemaleSurvivor);
            await assetProvider.Load<GameObject>(AssetsAdresses.Spawner);
            await assetProvider.Load<GameObject>(AssetsAdresses.MedKit);
        }

        public async Task<GameObject> CreateZombie(ZombieType type, Transform parent)
        {
            //todo make 1 method to load any prefab reference 
            ZombieConfig zombieConfig = configsService.ForZombie(type);
            //GameObject prefab = await assetProvider.Load<GameObject>(zombieConfig.PrefabReference);

            var container = await assetProvider.GetAssetContainer<GameObject>(zombieConfig.PrefabReference);
            var zombie = await container.CreateInstance(parent);

            IHealth zombieHealth = zombie.GetComponent<IHealth>();
            zombieHealth.CurrentHealth = zombieConfig.Hp;
            zombieHealth.MaxHealth = zombieConfig.Hp;
            zombie.GetComponent<ActorUI>().Initialize(zombieHealth);
            zombie.GetComponent<ZombieAttack>().Initialize(playerGOService.PlayerGameObject);
            zombie.GetComponent<ZombieFollow>().Initialize(playerGOService.PlayerGameObject);
            var death = zombie.GetComponent<ZombieDeath>();
            death.Initialize(gameProgressService.PlayerProgressData);
            TryInitializeZombieLootSpawner(zombie);

            return zombie;
        }

        private void TryInitializeZombieLootSpawner(GameObject zombie)
        {
            zombie.GetComponentInChildren<ZombieLootSpawner>()
                ?.ConstructMethod(ServiceLocator.Container.LocateService<IGamePrefabFactory>(), randomService);
        }

        public async Task<LootBehaviour> CreateLoot(LootType type)
        {
            GameObject prefab = null;
            switch (type)
            {
                case LootType.MedicalPack:
                    prefab = await assetProvider.Load<GameObject>(AssetsAdresses.MedKit);
                    break;
                case LootType.Grenade:
                    break;
                case LootType.FlashGrenade:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            var loot = Object.Instantiate(prefab);
            Register(loot);
            var lootBehaviour = loot.GetComponent<LootBehaviour>();
            lootBehaviour.Construct(gameProgressService.PlayerProgressData);
            return lootBehaviour;
        }

        public async void CreateZombieSpawner(Vector3 at, string id, ZombieType zombieType)
        {
            var prefab = await assetProvider.Load<GameObject>(AssetsAdresses.Spawner);
            SpawnPoint spawner = InstantiateRegisteredAsync(prefab, at).GetComponent<SpawnPoint>();

            spawner.Construct(this);
            spawner.Id = id;
            spawner.type = zombieType;
        }

        public async UniTask ZombieDeath(ZombieType type, GameObject zombie)
        {
            Debug.Log("Zomibe death handler in game prefab factory");
            ZombieConfig zombieConfig = configsService.ForZombie(type);
            var container = await assetProvider.GetAssetContainer<GameObject>(zombieConfig.PrefabReference);
            container.ReleaseInstance(zombie);
        }
    }
}