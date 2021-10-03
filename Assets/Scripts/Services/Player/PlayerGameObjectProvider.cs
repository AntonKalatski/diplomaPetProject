using System;
using UnityEngine;

namespace Services.Player
{
    public class PlayerGameObjectProvider : IPlayerGameObjectProvider
    {
        public GameObject PlayerGameObject { get; private set; }

        public void SetPlayerGameObject(GameObject player)
        {
            PlayerGameObject = player;
        }
    }
}