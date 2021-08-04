using Configs.Configs;
using UnityEngine;

namespace Providers
{
    public class LevelConfigProvider : MonoBehaviour
    {
        [SerializeField] private LevelConfig levelConfig;
        [SerializeField] private GameObject playerSpawnPoint;
        public GameObject GetSpawnPoint() => playerSpawnPoint;
    }
}