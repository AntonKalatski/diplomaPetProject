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

        #region InCourse

        private readonly Dictionary<string, AsyncOperationHandle> completedCashe =
            new Dictionary<string, AsyncOperationHandle>();

        private readonly Dictionary<string, List<AsyncOperationHandle>> completedHandles =
            new Dictionary<string, List<AsyncOperationHandle>>();

        public void InitializeAsync() => Addressables.InitializeAsync();

        public async Task<T> LoadAssetReference<T>(AssetReference assetReference) where T : Object
        {
            if (completedCashe.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<T>(assetReference),
                assetReference.AssetGUID);
        }

        public async Task<T> LoadAssetReference<T>(string address) where T : Object
        {
            if (completedCashe.TryGetValue(address, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<T>(address), cacheKey: address);
        }

        private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey)
            where T : Object
        {
            handle.Completed += h => { completedCashe[cacheKey] = h; };

            AddCompletedHandle(cacheKey, handle);

            return await handle.Task;
        }

        private void AddCompletedHandle<T>(string cacheKey, AsyncOperationHandle<T> handle) where T : Object
        {
            if (!completedHandles.TryGetValue(cacheKey, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                completedHandles[cacheKey] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }

        #endregion


        public async Task<T> Load<T>(AssetReference assetReference) where T : Object
        {
            if (handles.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
                return await completedHandle.Task as T;

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReference);

            AddHandle(assetReference.AssetGUID, handle);

            return await handle.Task;
        }

        public async Task<T> Load<T>(string address) where T : Object
        {
            if (handles.TryGetValue(address, out AsyncOperationHandle completedHandle))
                return await completedHandle.Task as T;

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);

            AddHandle(address, handle);

            return await handle.Task;
        }

        public void CleanUp()
        {
            foreach (var resourceHandles in completedHandles.Values)
            {
                foreach (var handle in resourceHandles)
                {
                    Addressables.Release(handle);
                }

                resourceHandles.Clear();
            }

            foreach (var operationHandle in handles.Values)
            {
                Addressables.Release(operationHandle);
            }

            handles.Clear();
            completedHandles.Clear();
            completedCashe.Clear();
        }

        public Task<GameObject> Instantiate(string address)
        {
            return Addressables.InstantiateAsync(address).Task;
        }

        public Task<GameObject> Instantiate(string address, Vector3 position)
        {
            return Addressables.InstantiateAsync(address, position, Quaternion.identity).Task;
        }

        public Task<GameObject> Instantiate(string address, Transform transform)
        {
            return Addressables.InstantiateAsync(address, transform).Task;
        }

        private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
        {
            if (handles.TryGetValue(key, out AsyncOperationHandle resourceHandles)) return;
            resourceHandles = handle;
            handles[key] = resourceHandles;
        }
    }
}