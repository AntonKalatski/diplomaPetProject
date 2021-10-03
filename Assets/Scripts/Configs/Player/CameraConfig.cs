using Configs.Container;
using UnityEngine;

namespace Configs.Player
{
    [CreateAssetMenu(menuName = "Game/Configs/CameraConfig", fileName = nameof(CameraConfig), order = 0)]
    public class CameraConfig : AbstractGameConfig
    {
        public string levelKey;
    }
}