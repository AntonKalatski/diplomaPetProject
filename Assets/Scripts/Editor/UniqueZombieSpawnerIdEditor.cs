using System;
using System.Linq;
using Spawner;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(UniqueSpawnerId))]
    public class UniqueZombieSpawnerIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueSpawnerId = (UniqueSpawnerId) target;

            if (IsPrefab(uniqueSpawnerId))
                return;

            if (string.IsNullOrEmpty(uniqueSpawnerId.SpawnerId))
                Generate(uniqueSpawnerId);
            else
            {
                UniqueSpawnerId[] uniqueIds = FindObjectsOfType<UniqueSpawnerId>();
                if (uniqueIds.Any(other => other != uniqueSpawnerId && other.SpawnerId == uniqueSpawnerId.SpawnerId))
                    Generate(uniqueSpawnerId);
            }
        }

        private bool IsPrefab(UniqueSpawnerId uniqueSpawnerId) => uniqueSpawnerId.gameObject.scene.rootCount == 0;

        private void Generate(UniqueSpawnerId uniqueSpawnerId)
        {
            uniqueSpawnerId.SpawnerId = $"{uniqueSpawnerId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";
            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueSpawnerId);
                EditorSceneManager.MarkSceneDirty(uniqueSpawnerId.gameObject.scene);
            }
        }
    }
}