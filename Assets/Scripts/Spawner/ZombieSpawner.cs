using System;
using GameData;
using Player;
using UnityEngine;

namespace Spawner
{
    public enum ZombieType
    {
        Walker
    }

    public class ZombieSpawner : MonoBehaviour, IProgressSaveable
    {
        [SerializeField] private ZombieType type;
        [SerializeField] private bool _isSlained;
        private string _id;

        private void Awake() => _id = GetComponent<UniqueSpawnerId>().SpawnerId;

        public void SaveProgress(PlayerProgressData progressData)
        {
            if (_isSlained)
                progressData.killData.clearedSpawners.Add(_id);
        }

        public void LoadProgress(PlayerProgressData progressData)
        {
            if (progressData.killData.clearedSpawners.Contains(_id))
                _isSlained = true;
            else
                Spawn();
        }

        private void Spawn()
        {
        }
    }
}