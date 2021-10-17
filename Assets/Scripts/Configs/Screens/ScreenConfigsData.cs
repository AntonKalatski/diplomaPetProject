using System.Collections.Generic;
using Configs.Container;
using UnityEngine;

namespace Configs.Screens
{
    [CreateAssetMenu(menuName = "Game/Configs/UI/ScreenConfigs", fileName = "ScreenConfigs", order = 0)]
    public class ScreenConfigsData : AbstractGameConfig
    {
        [SerializeField] private List<ScreenConfig> screens;
        public List<ScreenConfig> Screens => screens;
    }
}