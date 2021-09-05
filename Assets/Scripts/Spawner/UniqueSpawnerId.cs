using UnityEngine;

namespace Spawner
{
    public class UniqueSpawnerId : MonoBehaviour
    {
        [SerializeField] private string spawnerId;

        public string SpawnerId
        {
            get => spawnerId;
            set => spawnerId = value;
        }
    }
}