using UnityEngine;

namespace Factories.Interfaces
{
    public interface IGameUIFactory : IGameFactory
    {
        GameObject CreateHud();
    }
}