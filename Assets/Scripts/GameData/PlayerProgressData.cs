using System;

namespace GameData
{
    [Serializable]
    public class PlayerProgressData
    {
        public WorldData worldData;
        public PlayerState playerState;

        public PlayerProgressData(string initialLevel)
        {
            worldData = new WorldData(initialLevel);
            playerState = new PlayerState();
        }
    }
}