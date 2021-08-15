using Configs.Container;
using UnityEngine;

namespace Configs.GameConfigs
{
    [CreateAssetMenu(menuName = "Game/Configs/PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig : AbstractGameConfig
    {
        [SerializeField] private ConfigType type;
        [SerializeField] private float maxHp;
    }
}