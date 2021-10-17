using Services.GameServiceLocator;
using UI.Services;

namespace Factories.Interfaces
{
    public interface IScreenService : IService
    {
        void Open(ScreenType type);
    }
}