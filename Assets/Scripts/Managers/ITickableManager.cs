using Services.GameServiceLocator;

namespace Managers
{
    public interface ITickableManager : IService
    {
        void AddTickable(ITickable tickable);
        void RemoveTickable(ITickable tickable);
    }
}