using System;

namespace GameData
{
    [Serializable]
    public struct VectorStruct
    {
        public float X;
        public float Y;
        public float Z;

        public VectorStruct(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}