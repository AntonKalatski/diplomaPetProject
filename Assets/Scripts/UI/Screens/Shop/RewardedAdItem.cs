using System.Threading.Tasks;
using Services.Ads;
using Services.GameProgress;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.Shop
{
    public class RewardedAdItem : MonoBehaviour
    {
        [SerializeField] private Button showAdButton;
        [SerializeField] private GameObject[] adActiveObjects;
        [SerializeField] private GameObject[] adInactiveObjects;

        private IGameProgressService _progressService;
        private IAdsService _adsService;

        public void Construct(IAdsService adsService, IGameProgressService progressService)
        {
            _adsService = adsService;
            _progressService = progressService;
        }

        public void Initialize()
        {
            showAdButton.onClick.AddListener(() => OnShowAdClicked());

            RefreshAvailableAd();
        }

        public void Subscribe() => _adsService.AddOnRewardedVideoReadyListener(RefreshAvailableAd);

        public void CleanUp()
        {
            _adsService.RemoveOnRewardedVideoReadyListener(RefreshAvailableAd);
        }

        private async Task OnShowAdClicked()
        {
            var rewarded = await _adsService.ShowRewardedVideo();
            if (rewarded)
                OnRewardedVideoFinished();
        }

        private void RefreshAvailableAd()
        {
            var isAdsAvailable = _adsService.IsRewardedVideoReady();

            foreach (var adActive in adActiveObjects)
                adActive.SetActive(isAdsAvailable);
            foreach (var adInactive in adInactiveObjects)
                adInactive.SetActive(isAdsAvailable);
        }

        private void OnRewardedVideoFinished() =>
            _progressService.PlayerProgressData.killData.AddSkullCount(_adsService.Reward);
    }
}