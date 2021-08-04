using Factories.Interfaces;
using UnityEngine;

namespace Factories
{
    public interface IGamePrefabFactory : IGameFactory
    {
        GameObject CreateSurvivor(GameObject atPoint);
    }
}