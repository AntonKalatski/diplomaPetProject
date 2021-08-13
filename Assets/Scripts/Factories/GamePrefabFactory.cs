using System;
using Constants;
using Factories.Interfaces;
using Providers.Assets;
using Services.Player;
using UnityEngine;

namespace Factories
{
    public class GamePrefabFactory : AbstractGameFactory, IGamePrefabFactory
    {
        private readonly IPlayerGOService playerGOService;
        public GamePrefabFactory(IAssetProvider assetProvider, IPlayerGOService playerGOService) : base(assetProvider)
        {
            this.playerGOService = playerGOService;
        }

        public GameObject CreateSurvivor(GameObject atPoint)
        {
            var player = InstantiateRegistered(AssetsPath.FemaleSurvivor, atPoint.transform.position);
            playerGOService.SetPlayerGameObject(player);
            return player;
        }
    }
}