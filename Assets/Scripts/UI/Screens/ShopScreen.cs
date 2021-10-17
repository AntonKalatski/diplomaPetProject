using TMPro;
using UnityEngine;

namespace UI.Screens
{
    public class ShopScreen : BaseScreen
    {
        [SerializeField] private TMP_Text killCounter;
        protected override void Initialize() => RefreshKillCount();

        protected override void SubscribeUpdates() => Progress.killData.AddKillCounterListener(RefreshKillCount);

        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            Progress.killData.RemoveKillCounterListener(RefreshKillCount);
        }

        private void RefreshKillCount() => killCounter.text = Progress.killData.killedZombies.ToString();
    }
}