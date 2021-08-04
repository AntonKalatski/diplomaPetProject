using Configs.Container;
using UnityEngine;

namespace Configs.Configs
{
    [CreateAssetMenu(menuName = "Game/Configs/LevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : AbstractGameConfig
    {
        [SerializeField] private Transform playerSpawnPoint;

        public Transform PlayerSpawnPoint => playerSpawnPoint;
    }
}