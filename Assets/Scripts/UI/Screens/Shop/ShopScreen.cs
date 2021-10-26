using Services.Ads;
using Services.GameProgress;
using TMPro;
using UnityEngine;

namespace UI.Screens.Shop
{
    public class ShopScreen : BaseScreen
    {
        [SerializeField] private RewardedAdItem adItem;
        [SerializeField] private TMP_Text killCounter;

        public void Construct(IAdsService adsService, IGameProgressService progressService)
        {
            base.Construct(progressService);
            adItem.Construct(adsService,progressService);
        }

        protected override void Initialize()
        {
            adItem.Initialize();
            RefreshKillCount();
        }

        protected override void SubscribeUpdates()
        {
            adItem.Subscribe();
            Progress.killData.AddKillCounterListener(RefreshKillCount);
        }

        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            adItem.CleanUp();
            Progress.killData.RemoveKillCounterListener(RefreshKillCount);
        }

        private void RefreshKillCount() => killCounter.text = Progress.killData.killedZombies.ToString();
    }
}