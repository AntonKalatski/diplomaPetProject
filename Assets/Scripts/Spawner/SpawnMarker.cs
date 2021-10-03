using UnityEngine;

namespace Spawner
{
    public class SpawnMarker : MonoBehaviour
    {
        [SerializeField] private ZombieType type;

        public ZombieType ZombieType => type;
    }
}