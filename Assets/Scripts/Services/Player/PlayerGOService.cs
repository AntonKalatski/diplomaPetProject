using System;
using UnityEngine;

namespace Services.Player
{
    public class PlayerGOService : IPlayerGOService
    {
        private event Action<GameObject> OnPlayerGORefreshed;
        private GameObject playerGO;

       

        public void SetPlayerGameObject(GameObject player)
        {
            playerGO = player;
            OnPlayerGORefreshed?.Invoke(playerGO);
        }
        public GameObject GetPlayerGameObject() => playerGO;

        public void AddPlayerGORefreshListener(Action<GameObject> listener) => OnPlayerGORefreshed += listener;
        public void RemovePlayerGORefreshListener(Action<GameObject> listener)=> OnPlayerGORefreshed -= listener;
    }
}