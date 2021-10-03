using Behaviours.Loot;
using Configs.LootConfig;
using Spawner;
using UnityEngine;

namespace Factories.Interfaces
{
    public interface IGamePrefabFactory : IGameFactory
    {
        LootBehaviour CreateLoot(LootType type);
        GameObject CreateSurvivor(Vector3 atPoint);
        GameObject CreateZombie(ZombieType zombieType, Transform parent);
        void CreateZombieSpawner(Vector3 at, string id, ZombieType spawnerId);
    }
}