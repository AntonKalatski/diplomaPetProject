using Spawner;
using UnityEngine;

namespace Services.Configs.Zombie
{
    [System.Serializable]
    public class ZombieSpawnerData
    {
        public string Id;
        public ZombieType zombieType;
        public Vector3 Position;
        public ZombieSpawnerData(string id, ZombieType zombieType, Vector3 position)
        {
            Id = id;
            this.zombieType = zombieType;
            Position = position;
        }
    }
}