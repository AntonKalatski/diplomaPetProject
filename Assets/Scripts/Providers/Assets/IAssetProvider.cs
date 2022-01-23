using Cysharp.Threading.Tasks;
using Services.GameServiceLocator;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Providers.Assets
{
    public interface IAssetProvider : IService
    {
        //Initialization

        UniTask InitializeAsync();

        //load 
        UniTask<T> Load<T>(AssetReference assetReference) where T : Object;

        UniTask<T> Load<T>(string address) where T : Object;

        UniTask<GameObject> Instantiate(string path);

        UniTask<GameObject> InstantiateAsync(AssetReference reference, Transform parent);

        UniTask<GameObject> Instantiate(string path, Vector3 position);

        UniTask<GameObject> Instantiate(string prefabPath, Transform transform);

        public void CleanUp();

        // Task<T> LoadAssetReference<T>(AssetReference assetReference) where T : Object;
        // Task<T> LoadAssetReference<T>(string address) where T : Object;
        UniTask<GameObject> CreateInstance(string address);

        UniTask<AssetContainer<T>> GetAssetContainer<T>(AssetReference assetReference) where T : Object;
    }
}