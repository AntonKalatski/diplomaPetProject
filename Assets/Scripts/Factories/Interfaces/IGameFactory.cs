using System.Collections.Generic;
using Player;
using Services.GameServiceLocator;
using UnityEngine;

namespace Factories.Interfaces
{
    public interface IGameFactory : IService
    {
        List<IProgressLoadable> ProgressLoadables { get; }
        List<IProgressSaveable> ProgressSaveables { get; }
        void Register(GameObject gameObject);
        void CleanUp();
    }
}