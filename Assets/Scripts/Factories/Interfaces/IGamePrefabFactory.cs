using System.Threading.Tasks;
using Behaviours.Loot;
using Configs.LootConfig;
using Spawner;
using UnityEngine;

namespace Factories.Interfaces
{
    public interface IGamePrefabFactory : IGameFactory
    {
        Task WarmUp();
        Task<GameObject> CreateSurvivor(Vector3 atPoint);
        Task<LootBehaviour> CreateLoot(LootType type);
        Task<GameObject> CreateZombie(ZombieType zombieType, Transform parent);
        void CreateZombieSpawner(Vector3 at, string id, ZombieType spawnerId);
    }
}