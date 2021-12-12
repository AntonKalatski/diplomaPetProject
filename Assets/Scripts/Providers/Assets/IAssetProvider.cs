using System.Threading.Tasks;
using Services.GameServiceLocator;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Providers.Assets
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 position);
        GameObject Instantiate(string prefabPath, Transform transform);
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        public void CleanUp();
    }
}