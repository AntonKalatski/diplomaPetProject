using System;
using UnityEngine;

namespace Factories.Interfaces
{
    public interface IGamePrefabFactory : IGameFactory
    {
        GameObject CreateSurvivor(Vector3 atPoint);
    }
}