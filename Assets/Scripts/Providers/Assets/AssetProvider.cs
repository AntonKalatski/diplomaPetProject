using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Providers.Assets
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> handles =
            new Dictionary<string, AsyncOperationHandle>();

        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            if (handles.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
            {
                Debug.Log("Found asset referense in completed cache");
                return await completedHandle.Task as T;
            }


            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReference);

            AddHandle(assetReference.AssetGUID, handle);

            return await handle.Task;
        }

        private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
        {
            if (handles.TryGetValue(key, out AsyncOperationHandle resourceHandles)) return;
            resourceHandles = handle;
            handles[key] = resourceHandles;
        }

        public void CleanUp()
        {
            foreach (var operationHandle in handles.Values)
            {
                Addressables.Release(operationHandle);
            }
            handles.Clear();
        }

        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 position)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, position, Quaternion.identity);
        }

        public GameObject Instantiate(string path, Transform transform)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, transform);
        }
    }
}