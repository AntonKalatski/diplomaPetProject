using System.Threading.Tasks;
using Services.GameServiceLocator;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Providers.Assets
{
    public interface IAssetProvider : IService
    {
        Task<GameObject> Instantiate(string path);
        Task<GameObject> Instantiate(string path, Vector3 position);
        Task<GameObject> Instantiate(string prefabPath, Transform transform);
        Task<T> Load<T>(AssetReference assetReference) where T : Object;
        Task<T> Load<T>(string address) where T : Object;
        public void CleanUp();
        Task<T> LoadAssetReference<T>(AssetReference assetReference) where T : Object;
        Task<T> LoadAssetReference<T>(string address) where T : Object;
        void InitializeAsync();
    }
}