using Providers.Assets;
using Services.Ads;
using Services.GameProgress;
//using Services.IAp;
using TMPro;
using UnityEngine;

namespace UI.Screens.Shop
{
    public class ShopScreen : BaseScreen
    {
        [SerializeField] private RewardedAdItem adItem;
        [SerializeField] private TMP_Text killCounter;
        [SerializeField] private ShopItemsContainer shopItems;

        public void Construct(
            IAdsService adsService,
            IGameProgressService progressService,
            //IInAppService inAppService,
            IAssetProvider assetProvider)
        {
            base.Construct(progressService);
            adItem.Construct(adsService, progressService);
            shopItems.Construct(progressService, assetProvider);
        }

        protected override void Initialize()
        {
            adItem.Initialize();
            shopItems.Initialize();
            RefreshKillCount();
        }

        protected override void SubscribeUpdates()
        {
            adItem.Subscribe();
            shopItems.Subscribe();
            Progress.killData.AddKillCounterListener(RefreshKillCount);
        }

        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            adItem.CleanUp();
            shopItems.CleanUp();
            Progress.killData.RemoveKillCounterListener(RefreshKillCount);
        }

        private void RefreshKillCount() => killCounter.text = Progress.killData.killedZombies.ToString();
    }
}