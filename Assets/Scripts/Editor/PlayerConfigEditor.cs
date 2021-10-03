using Configs.Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    [CustomEditor(typeof(PlayerConfig))]
    public class PlayerConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            PlayerConfig playerConfig = (PlayerConfig) target;

            if (GUILayout.Button("Collect spawners"))
            {
                playerConfig.playerSpawnPoint =
                    new PlayerSpawnPointData(FindObjectOfType<PlayerSpawnPoint>().transform.position);
                playerConfig.levelKey = SceneManager.GetActiveScene().name;
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}