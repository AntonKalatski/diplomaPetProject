using Factories.Interfaces;

namespace Factories
{
    public interface IGameUIFactory : IGameFactory
    {
        void CreateHub();
    }
}