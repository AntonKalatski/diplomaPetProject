using UnityEngine;

namespace Configs.Player
{
    [System.Serializable]
    public struct PlayerSpawnPointData
    {
        public Vector3 position;

        public PlayerSpawnPointData(Vector3 position)
        {
            this.position = position;
        }
    }
}