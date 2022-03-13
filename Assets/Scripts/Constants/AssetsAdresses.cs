using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public static class AssetsAdresses
    {
        public const string FemaleSurvivor = "Female_Survivor";
        public const string Hud = "Hud";
        public const string UIRoot = "UIRoot";
        public const string MedKit = "MedKit";
        public const string Spawner = "SpawnPoint";
        public static string MainMenu = "MainMenu";

        public static T RandomValue<T>(IReadOnlyList<T> collection) => collection[Random.Range(0, collection.Count)];
    }
}