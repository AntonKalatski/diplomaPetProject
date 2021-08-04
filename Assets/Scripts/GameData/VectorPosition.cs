using System;
using UnityEngine;

namespace GameData
{
    [Serializable]
    public struct VectorPosition
    {
        public float X;
        public float Y;
        public float Z;

        public VectorPosition(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 AsUnityVector()
        {
            return new Vector3(X, Y, Z);
        }
    }
}