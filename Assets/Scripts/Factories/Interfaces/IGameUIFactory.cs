using System.Threading.Tasks;
using UnityEngine;

namespace Factories.Interfaces
{
    public interface IGameUIFactory : IGameFactory
    {
        void CreateUIRoot();
        Task<GameObject> CreateHud();
        void CreateShop();
        Task WarmUp();
    }
}