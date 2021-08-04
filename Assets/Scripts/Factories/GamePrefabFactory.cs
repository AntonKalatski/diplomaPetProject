using Constants;
using Providers.Assets;
using UnityEngine;

namespace Factories
{
    public class GamePrefabFactory : IGamePrefabFactory
    {
        private readonly IAssetProvider assetProvider;
        public GamePrefabFactory(IAssetProvider assetProvider) => this.assetProvider = assetProvider;
        public GameObject CreateSurvivor(GameObject atPoint) =>
            assetProvider.Instantiate(AssetsPath.FemaleSurvivor, position: atPoint.transform.position);
    }
}