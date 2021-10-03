using System;
using Services.GameServiceLocator;
using UnityEngine;

namespace Services.Player
{
    public interface IPlayerGameObjectProvider : IService
    {
        GameObject PlayerGameObject { get; }
        void SetPlayerGameObject(GameObject player);
    }
}