using Constants;
using Factories.Interfaces;
using Providers.Assets;
using UnityEngine;

namespace Factories
{
    public class GamePrefabFactory : AbstractGameFactory, IGamePrefabFactory
    {
        public GamePrefabFactory(IAssetProvider assetProvider) : base(assetProvider)
        {
        }

        public GameObject CreateSurvivor(GameObject atPoint) =>
            InstantiateRegistered(AssetsPath.FemaleSurvivor, atPoint.transform.position);
    }
}