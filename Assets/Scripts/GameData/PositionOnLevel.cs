using System;

namespace GameData
{
    [Serializable]
    public struct PositionOnLevel
    {
        public string Level;
        public VectorPosition Position;

        public PositionOnLevel(string level, VectorPosition position)
        {
            Level = level;
            Position = position;
        }
    }
}