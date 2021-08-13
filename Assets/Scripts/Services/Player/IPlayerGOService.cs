using Services.GameServiceLocator;
using UnityEngine;

namespace Services.Player
{
    public interface IPlayerGOService : IService
    {
        void SetPlayerGameObject(GameObject player);
        GameObject GetPlayerGameObject();
        Transform GetPlayerTransform();
    }
}