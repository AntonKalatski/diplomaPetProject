using Configs.Container;
using UnityEngine;

namespace Configs.Player
{
    [CreateAssetMenu(menuName = "Game/Configs/PlayerConfig", fileName = nameof(PlayerConfig), order = 0)]
    public class PlayerConfig : AbstractGameConfig
    {
        public string levelKey;
        public PlayerSpawnPointData playerSpawnPoint;
    }
}