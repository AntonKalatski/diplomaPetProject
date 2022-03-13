using Spawner;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Services.Configs
{
    [System.Serializable]
    public class ZombieSpawnerData
    {
        public string Id;
        public ZombieType zombieType;
        public Vector3 Position;
        public AssetReference spawnerReference;
        public ZombieSpawnerData(string id, ZombieType zombieType, Vector3 position)
        {
            Id = id;
            this.zombieType = zombieType;
            Position = position;
        }
    }
}