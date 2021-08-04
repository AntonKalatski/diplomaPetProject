using Constants;
using Providers.Assets;

namespace Factories
{
    public class GameUIFactory : IGameUIFactory
    {
        private readonly IAssetProvider assetProvider;

        public GameUIFactory(IAssetProvider assetProvider) => this.assetProvider = assetProvider;
        public void CreateHub() => assetProvider.Instantiate(AssetsPath.Hud);
    }
}