using Constants;
using Factories.Interfaces;
using Providers.Assets;
using Services.GameProgress;
using UI.Bars;
using UnityEngine;

namespace Factories
{
    public class GameUIFactory : AbstractGameFactory, IGameUIFactory
    {
        private readonly IGameProgressService progressService;

        public GameUIFactory(IGameProgressService progressService,IAssetProvider assetProvider) : base(assetProvider)
        {
            this.progressService = progressService;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetsPath.Hud);
            hud.GetComponentInChildren<KillCounterBar>().Construct(progressService.PlayerProgressData);
            return hud;
        }
    }
}