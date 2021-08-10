using GameData;

namespace Player
{
    public interface IProgressSaveable : IProgressLoadable
    {
        void SaveProgress(PlayerProgressData progressData);
    }
}