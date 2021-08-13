using UnityEngine;

namespace Services.Player
{
    public class PlayerGOService : IPlayerGOService
    {
        private GameObject playerGO;

        public PlayerGOService()
        {
        }

        public void SetPlayerGameObject(GameObject player) => playerGO = player;

        public GameObject GetPlayerGameObject() => playerGO;

        public Transform GetPlayerTransform() => playerGO.transform;
    }
}