using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public static class AssetsPath
    {
        #region Survivors

        public const string FemaleSurvivor = "Player/Characters/Female_Survivor";

        #endregion

        #region UIPrefabs

        public const string Hud = "UI/Hud/Hud";

        #endregion

        #region Loot
        public const string Loot = "Loot/Loot";
        #endregion

        public static readonly List<string> ZomibesList = new List<string>()
        {
            "Player/Characters/Female_Survivor",
            "Player/Characters/Female_Survivor",
        };

        public const string Spawner = "Zombie/SpawnPoint";

        public static T RandomValue<T>(IReadOnlyList<T> collection) => collection[Random.Range(0, collection.Count)];
    }
}