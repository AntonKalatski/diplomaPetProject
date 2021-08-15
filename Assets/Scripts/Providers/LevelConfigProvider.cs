using UnityEngine;

namespace Providers
{
    public class LevelConfigProvider : MonoBehaviour
    {
        [SerializeField] private GameObject playerSpawnPoint;
        public GameObject GetSpawnPoint() => playerSpawnPoint;
    }
}