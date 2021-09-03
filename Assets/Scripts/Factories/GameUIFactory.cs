using Constants;
using Factories.Interfaces;
using Providers.Assets;
using UnityEngine;

namespace Factories
{
    public class GameUIFactory : AbstractGameFactory, IGameUIFactory
    {
        public GameUIFactory(IAssetProvider assetProvider) : base(assetProvider)
        {
        }

        public GameObject CreateHud() => InstantiateRegistered(AssetsPath.Hud);
    }
}