using System;
using UnityEngine;

namespace GameData
{
    [Serializable]
    public class PlayerProgressData
    {
        public WorldData worldData;
        public PlayerState playerState;
        public PlayerStats playerStats;
        public KillData killData;
        public LootData lootData;
        public PurchaseData purchaseData;

        public PlayerProgressData(string initialLevel)
        {
            worldData = new WorldData(initialLevel);
            playerState = new PlayerState();
            playerStats = new PlayerStats();
            killData = new KillData();
            lootData = new LootData();
            purchaseData = new PurchaseData();
        }

    }
}