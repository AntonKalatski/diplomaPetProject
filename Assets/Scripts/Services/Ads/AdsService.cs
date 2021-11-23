using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Services.Ads
{
    public class AdsService : IAdsService, IUnityAdsListener
    {
        private const string AndroidGameId = "4422311";
        private const string IosGameId = "4422310";
        private const string RewardedVideoPlacementIdAndroid = "Rewarded_Android";
        private const string RewardedVideoPlacementIdIos = "Rewarded_iOS";
        
        private TaskCompletionSource<bool> taskCompletionSource;

        private string rewardedPlacementId;
        private string gameId;

        public event Action RewardedVideoReady;
        public int Reward => 100;


        public void Initialize()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    gameId = AndroidGameId;
                    rewardedPlacementId = RewardedVideoPlacementIdAndroid;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    gameId = IosGameId;
                    rewardedPlacementId = RewardedVideoPlacementIdIos;
                    break;
                case RuntimePlatform.WindowsEditor:
                    gameId = AndroidGameId;
                    rewardedPlacementId = RewardedVideoPlacementIdAndroid;
                    break;
                default:
                    Debug.Log("Unsupported Platform for Ads");
                    break;
            }
            Advertisement.AddListener(this);
            Advertisement.Initialize(gameId);
        }

        public void AddOnRewardedVideoReadyListener(Action listener) => RewardedVideoReady += listener;
        public void RemoveOnRewardedVideoReadyListener(Action listener) => RewardedVideoReady -= listener;

        public Task<bool> ShowRewardedVideo()
        {
            Advertisement.Show(placementId: rewardedPlacementId);
            taskCompletionSource = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            return taskCompletionSource.Task;
        }

        public bool IsRewardedVideoReady() => Advertisement.IsReady(rewardedPlacementId);

        public void OnUnityAdsReady(string placementId)
        {
            Debug.Log($"OnUnityAdsReady {placementId}");
            if (placementId.Equals(rewardedPlacementId))
                RewardedVideoReady?.Invoke();
        }

        public void OnUnityAdsDidError(string message) =>
            Debug.Log($"OnUnityAdsDidError {message}");

        public void OnUnityAdsDidStart(string placementId) =>
            Debug.Log($"OnUnityAdsDidStart {placementId}");

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            switch (showResult)
            {
                case ShowResult.Failed:
                    Debug.LogError($"OnUnityAdsDidFinish {showResult}");
                    taskCompletionSource.SetResult(false);
                    break;
                case ShowResult.Skipped:
                    Debug.LogError($"OnUnityAdsDidFinish {showResult}");
                    taskCompletionSource.SetResult(false);
                    break;
                case ShowResult.Finished:
                    taskCompletionSource.SetResult(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(showResult), showResult, null);
            }
        }
    }
}