using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Providers.Assets
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AbstractAssetContainer> assetContainers =
            new Dictionary<string, AbstractAssetContainer>();

        private readonly Dictionary<string, AsyncOperationHandle> handles =
            new Dictionary<string, AsyncOperationHandle>();

        public UniTask InitializeAsync()
        {
            return Addressables.InitializeAsync().ToUniTask();
        }

        public async UniTask<AssetContainer<T>> GetAssetContainer<T>(AssetReference assetReference) where T : Object
        {
            var key = assetReference.AssetGUID;
            if (!assetContainers.ContainsKey(key))
                await InitializeAssetContainer<T>(assetReference, key);

            return assetContainers[key] as AssetContainer<T>;
        }

        private async UniTask InitializeAssetContainer<T>(AssetReference assetReference, string key)
            where T : Object
        {
            AssetContainer<T> container = new AssetContainer<T>(assetReference);
            assetContainers[key] = container;
        }

        public void CleanUp()
        {
            Debug.Log("Clean up");

            foreach (var operationHandle in handles.Values)
                Addressables.Release(operationHandle);
            foreach (var reference in assetContainers.Values)
                reference.ReleaseContainer();

            handles.Clear();
            assetContainers.Clear();
        }

        public UniTask<GameObject> CreateInstance(AssetReference reference) =>
            Addressables.InstantiateAsync(reference).ToUniTask();

        public async UniTask<T> Load<T>(AssetReference assetReference) where T : Object
        {
            if (handles.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
                return await completedHandle.Task as T;

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReference);

            AddHandle(assetReference.AssetGUID, handle);

            return await handle.Task;
        }

        public async UniTask<T> Load<T>(string address) where T : Object
        {
            if (handles.TryGetValue(address, out AsyncOperationHandle completedHandle))
                return await completedHandle.Task as T;

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);

            AddHandle(address, handle);

            return await handle.Task;
        }

        public UniTask<GameObject> InstantiateAsync(AssetReference reference, Transform parent)
        {
            return Addressables.InstantiateAsync(reference, parent).ToUniTask();
        }

        public UniTask<GameObject> CreateInstance(string address)
        {
            if (handles.ContainsKey(address))
            {
                return Addressables.InstantiateAsync(handles[address]).ToUniTask();
            }

            var handle = Addressables.LoadAssetAsync<GameObject>(address);
            AddHandle(address, handle);
            return Addressables.InstantiateAsync(address).ToUniTask();
        }

        public UniTask<GameObject> Instantiate(string address)
        {
            return Addressables.InstantiateAsync(address).ToUniTask();
        }

        public UniTask<GameObject> Instantiate(string address, Vector3 position) =>
            Addressables.InstantiateAsync(address, position, Quaternion.identity).ToUniTask();

        public UniTask<GameObject> Instantiate(string address, Transform transform) =>
            Addressables.InstantiateAsync(address, transform).ToUniTask();

        private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
        {
            if (handles.TryGetValue(key, out AsyncOperationHandle resourceHandles)) return;
            resourceHandles = handle;
            handles[key] = resourceHandles;
        }
    }
}
// public Task<GameObject> Instantiate(AssetReference reference, Transform transform)
// {
// var handle = Addressables.InstantiateAsync(reference,transform);
// handle.Completed += (x) => test.Add(reference.AssetGUID, x);
// return handle.Task;
// }


// #region InCourse
//
// private readonly Dictionary<string, AsyncOperationHandle> completedCashe =
//     new Dictionary<string, AsyncOperationHandle>();
//
// private readonly Dictionary<string, List<AsyncOperationHandle>> completedHandles =
//     new Dictionary<string, List<AsyncOperationHandle>>();
//
// 
//
// public async Task<T> LoadAssetReference<T>(AssetReference assetReference) where T : Object
// {
//     if (completedCashe.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
//         return completedHandle.Result as T;
//
//     return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<T>(assetReference),
//         assetReference.AssetGUID);
// }
//
// public async Task<T> LoadAssetReference<T>(string address) where T : Object
// {
//     if (completedCashe.TryGetValue(address, out AsyncOperationHandle completedHandle))
//         return completedHandle.Result as T;
//
//     return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<T>(address), cacheKey: address);
// }
//
// private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey)
//     where T : Object
// {
//     handle.Completed += h => { completedCashe[cacheKey] = h; };
//
//     AddCompletedHandle(cacheKey, handle);
//
//     return await handle.Task;
// }
//
// private void AddCompletedHandle<T>(string cacheKey, AsyncOperationHandle<T> handle) where T : Object
// {
//     if (!completedHandles.TryGetValue(cacheKey, out List<AsyncOperationHandle> resourceHandles))
//     {
//         resourceHandles = new List<AsyncOperationHandle>();
//         completedHandles[cacheKey] = resourceHandles;
//     }
//
//     resourceHandles.Add(handle);
// }
//
// #endregion