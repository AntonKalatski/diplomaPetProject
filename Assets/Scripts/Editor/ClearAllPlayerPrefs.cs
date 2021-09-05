using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ClearAllPlayerPrefs
    {
        [MenuItem("Utils/ClearPrefs")]
        public static void ClearPrefs()
        {
            Debug.LogWarning("All player prefs are cleared!");
            PlayerPrefs.DeleteAll();
        }
    }
}