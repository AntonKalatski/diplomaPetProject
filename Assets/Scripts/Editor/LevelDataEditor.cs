using System.Linq;
using Services.Configs.Zombie;
using Spawner;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    [CustomEditor(typeof(LevelData))]
    public class LevelDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelData levelData = (LevelData) target;

            if (GUILayout.Button("Collect spawners"))
            {
                levelData.zombieSpawners = FindObjectsOfType<SpawnMarker>().Select(x =>
                    new ZombieSpawnerData(x.GetComponent<UniqueSpawnerId>().SpawnerId, x.ZombieType,
                        x.transform.position)).ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}