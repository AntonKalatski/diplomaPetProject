using Configs.LootConfig;
using Factories.Interfaces;
using GameData;
using Services.Random;
using UnityEngine;

namespace Zombies
{
    public class ZombieLootSpawner : MonoBehaviour
    {
        [SerializeField] private ZombieDeath zombieDeath;
        [SerializeField] private LootType lootToSpawn;
        [SerializeField, Range(0, 1f)] private float chanceToSpawnLoot;
        private IGamePrefabFactory prefabFactory;
        private IRandomService randomService;

        public void ConstructMethod(
            IGamePrefabFactory prefabFactory,
            IRandomService randomService)
        {
            this.randomService = randomService;
            this.prefabFactory = prefabFactory;
        }

        private void Start() => zombieDeath.AddOnDeathListener(ZombieDeathHandler);

        private void ZombieDeathHandler()
        {
            zombieDeath.RemoveOnDeathListener(ZombieDeathHandler);
            // if (randomService.CalculateChance(0, 1, chanceToSpawnLoot))
            InitializeLootBehaviour();
        }

        private void InitializeLootBehaviour()
        {
            //todo make new loot item 
            var loot = prefabFactory.CreateLoot(lootToSpawn);
            loot.transform.position = transform.position;
            loot.Initialize(new LootItem()
            {
                type = lootToSpawn,
                value = 1
            });
        }
    }
}