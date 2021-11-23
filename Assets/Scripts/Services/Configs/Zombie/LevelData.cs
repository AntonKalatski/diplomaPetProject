using System.Collections.Generic;
using UnityEngine;

namespace Services.Configs.Zombie
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "LevelData/Level")]
    public class LevelData : ScriptableObject
    {
        public string LevelKey;
        public List<ZombieSpawnerData> zombieSpawners;
        public Vector3 InitialHeroPosition;
    }
}