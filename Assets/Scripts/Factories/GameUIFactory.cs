using Constants;
using Factories.Interfaces;
using Providers.Assets;

namespace Factories
{
    public class GameUIFactory : AbstractGameFactory, IGameUIFactory
    {
        public GameUIFactory(IAssetProvider assetProvider) : base(assetProvider)
        {
        }

        public void CreateHub() => InstantiateRegistered(AssetsPath.Hud);
    }
}