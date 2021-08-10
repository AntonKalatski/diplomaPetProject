using System;

namespace GameData
{
    [Serializable]
    public class PositionOnLevel
    {
        public string level;
        public VectorStruct position;
        public VectorStruct rotation;

        public PositionOnLevel(string level, VectorStruct position, VectorStruct rotation)
        {
            this.level = level;
            this.position = position;
            this.rotation = rotation;
        }
        public PositionOnLevel(string initialLevel) => level = initialLevel;
    }
}