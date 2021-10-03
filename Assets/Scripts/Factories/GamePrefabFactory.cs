using Behaviours.Loot;
using Configs.LootConfig;
using Constants;
using Factories.Interfaces;
using GameElements.Health;
using Providers.Assets;
using Services.Configs.Zombie;
using Services.GameProgress;
using Services.GameServiceLocator;
using Services.Player;
using Services.Random;
using Spawner;
using UI.Actors;
using UnityEngine;
using Zombies;

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

        public GameObject CreateSurvivor(Vector3 atPoint)
        {
            var player = InstantiateRegistered(AssetsPath.FemaleSurvivor, atPoint);
            playerGOService.SetPlayerGameObject(player);
            return player;
        }

        public GameObject CreateZombie(ZombieType type, Transform parent)
        {
            var zombieConfig = configsService.ForZombie(type);
            var zombie = Object.Instantiate(zombieConfig.ZombiePrefab, parent);
            var zombieHealth = zombie.GetComponent<IHealth>();
            zombieHealth.CurrentHealth = zombieConfig.Hp;
            zombieHealth.MaxHealth = zombieConfig.Hp;
            zombie.GetComponent<ActorUI>().Initialize(zombieHealth);
            zombie.GetComponent<ZombieAttack>().Initialize(playerGOService.PlayerGameObject);
            zombie.GetComponent<ZombieFollow>().Initialize(playerGOService.PlayerGameObject);
            zombie.GetComponent<ZombieDeath>().Initialize(gameProgressService.PlayerProgressData);

            TryInitializeZombieLootSpawner(zombie);

            return zombie;

            //todo rework for resourses load unload
            // return Instantiate(AssetsPath.RandomValue<string>(AssetsPath.ZomibesList),parent);
        }

        private void TryInitializeZombieLootSpawner(GameObject zombie)
        {
            zombie.GetComponentInChildren<ZombieLootSpawner>()
                ?.ConstructMethod(ServiceLocator.Container.LocateService<IGamePrefabFactory>(), randomService);
        }

        public LootBehaviour CreateLoot(LootType type)
        {
            var loot = Object.Instantiate(configsService.ForLoot(type).LootObject);
            Register(loot);
            var lootBehaviour = loot.GetComponent<LootBehaviour>();
            lootBehaviour.Construct(gameProgressService.PlayerProgressData);
            return lootBehaviour;
        }

        public void CreateZombieSpawner(Vector3 at, string id, ZombieType zombieType)
        {
            SpawnPoint spawner = InstantiateRegistered(AssetsPath.Spawner, at).GetComponent<SpawnPoint>();
            spawner.Construct(this);
            spawner.Id = id;
            spawner.type = zombieType;
        }
    }
}