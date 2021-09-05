using System.Collections.Generic;
using Spawner;
using UnityEngine;

namespace Providers
{
    public class ZombieSpawnersProvider : MonoBehaviour
    {
        [SerializeField] private List<ZombieSpawner> zombieSpawners;
        public List<ZombieSpawner> GetZombieSpawners() => zombieSpawners;
    }
}