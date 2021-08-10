using System;

namespace GameData
{
    [Serializable]
    public class PlayerProgressData
    {
        public WorldData worldData;

        public PlayerProgressData(string initialLevel)
        {
            worldData = new WorldData(initialLevel);
        }
    }
}