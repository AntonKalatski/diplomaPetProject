using System;
using Services.GameServiceLocator;
using UnityEngine;

namespace Services.Player
{
    public interface IPlayerGOService : IService
    {
        void SetPlayerGameObject(GameObject player);
        void AddPlayerGORefreshListener(Action<GameObject> listener);
        void RemovePlayerGORefreshListener(Action<GameObject> listener);
    }
}