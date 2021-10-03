using System;

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

        public PlayerProgressData(string initialLevel)
        {
            worldData = new WorldData(initialLevel);
            playerState = new PlayerState();
            playerStats = new PlayerStats();
            killData = new KillData();
            lootData = new LootData();
        }

    }
}