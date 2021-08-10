using System.Collections.Generic;
using Player;
using Services.GameServiceLocator;

namespace Factories.Interfaces
{
    public interface IGameFactory : IService
    {
        List<IProgressLoadable> ProgressLoadables { get; }
        List<IProgressSaveable> ProgressSaveables { get; }
        void CleanUp();
    }
}