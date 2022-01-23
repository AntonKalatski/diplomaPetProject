﻿using Factories.Interfaces;
using GameData;
using Player;
using UnityEngine;
using Zombies;

namespace Spawner
{
    public class SpawnPoint : MonoBehaviour, IProgressSaveable
    {
        public ZombieType type;
        public bool isSlained;
        public string Id { get; set; }
        private IGamePrefabFactory factory;
        private ZombieDeath zombieDeath;
        private GameObject zombie;

        public void Construct(IGamePrefabFactory factory) => this.factory = factory;

        public void SaveProgress(PlayerProgressData progressData)
        {
            Debug.Log("SaveProgress in zombie spawner");
            if (isSlained)
            {
                progressData.killData.clearedSpawners.Add(Id);
            }
        }

        public void LoadProgress(PlayerProgressData progressData)
        {
            Debug.Log("LoadProgress in zombie spawner");
            if (progressData.killData.clearedSpawners.Contains(Id))
                isSlained = true;
            else
                Spawn();
        }

        private async void Spawn()
        {
            Debug.Log("Try to spawn zombie");
           zombie = await factory.CreateZombie(type, transform);
            zombieDeath = zombie.GetComponent<ZombieDeath>();
            zombieDeath.AddOnDeathListener(ZombieDeathHandler);
        }

        private void ZombieDeathHandler()
        {
            Debug.Log("ZombieDeathHandler");
            if (!ReferenceEquals(zombieDeath, null))
                zombieDeath.RemoveOnDeathListener(ZombieDeathHandler);
            isSlained = true;
            factory.ZombieDeath(type,zombie);
        }
    }
}