using UnityEngine;

namespace Factories.Interfaces
{
    public interface IGamePrefabFactory : IGameFactory
    {
        GameObject CreateSurvivor(GameObject atPoint);
       
    }
}