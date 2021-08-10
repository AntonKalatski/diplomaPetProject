using GameData;
using UnityEngine;

namespace Extensions
{
    public static class GameDataExtensions
    {
        public static VectorStruct AsVectorPosition(this Vector3 pos) => new VectorStruct(pos.x, pos.y, pos.z);
        public static Vector3 AsUnityVector3(this VectorStruct vectPos) =>
            new Vector3(vectPos.X, vectPos.Y, vectPos.Z);

        public static Vector3 AddY(this Vector3 vector, float y)
        {
            vector.y += y;
            return vector;
        }

        public static string ToJson(this object obj) => JsonUtility.ToJson(obj);
        public static T ToDeserialized<T>(this string json) => JsonUtility.FromJson<T>(json);
    }
}