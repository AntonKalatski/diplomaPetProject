using GameData;
using UnityEngine;

namespace Extensions
{
    public static class GameDataExtensions
    {
        public static VectorPosition AsVectorPosition(this Vector3 pos) => new VectorPosition(pos.x, pos.y, pos.z); 
        public static Vector3 AsUnityVector3(this VectorPosition vectPos) => new Vector3(vectPos.X, vectPos.Y, vectPos.Z); 
    }
}