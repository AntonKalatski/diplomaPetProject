using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Configs.Container
{
    [CreateAssetMenu(menuName = "Game/Configs/GameConfigsContainer", fileName = "GameConfigsContainer", order = 0)]
    public class GameConfigsContainer : ScriptableObject
    {
        [SerializeField] private List<AbstractGameConfig> configs;

        public TConfig GetConfig<TConfig>(ConfigType type) where TConfig : AbstractGameConfig
        {
            return configs.First(x => x.Type == type) as TConfig;
        }
    }
}