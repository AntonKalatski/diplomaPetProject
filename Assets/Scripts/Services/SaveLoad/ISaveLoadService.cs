using GameData;
using Services.GameServiceLocator;

namespace Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgressData LoadProgress();
    }
}