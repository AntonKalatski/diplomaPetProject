using GameData;

namespace Player
{
    public interface IProgressUpdatable : IProgressLoadable
    {
        void UpdateProgress(PlayerProgressData progressData);
    }
}