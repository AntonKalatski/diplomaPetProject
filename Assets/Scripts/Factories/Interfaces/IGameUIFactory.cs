using UnityEngine;

namespace Factories.Interfaces
{
    public interface IGameUIFactory : IGameFactory
    {
        void CreateUIRoot();
        GameObject CreateHud();
        void CreateShop();
    }
}