using System;
using System.Threading.Tasks;
using Services.GameServiceLocator;

namespace Services.Ads
{
    public interface IAdsService : IService
    {
        void Initialize();
        Task<bool> ShowRewardedVideo();
        bool IsRewardedVideoReady();
        void AddOnRewardedVideoReadyListener(Action listener);
        void RemoveOnRewardedVideoReadyListener(Action listener);
        int Reward { get;}
    }
}