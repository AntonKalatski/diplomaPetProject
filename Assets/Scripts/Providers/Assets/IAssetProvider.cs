using Services.GameServiceLocator;
using UnityEngine;

namespace Providers.Assets
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 position);
    }
}