using UnityEngine;

namespace Configs.Container
{
    public abstract class AbstractGameConfig : ScriptableObject
    {
        [SerializeField] private ConfigType type;
        public ConfigType Type => type;
    }
}