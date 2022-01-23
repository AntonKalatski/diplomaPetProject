using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Providers.Assets
{
    public class AssetContainer<T> : AbstractAssetContainer where T : Object
    {
        private readonly AssetReference _reference;
        private AsyncOperationHandle<T> _handle;
        private readonly T _asset;

        private readonly List<GameObject> _instances = new List<GameObject>();

        public AssetContainer(AssetReference reference)
        {
            _reference = reference;
        }

        public async UniTask<GameObject> CreateInstance(Transform parent = null)
        {
            var instance = await Addressables.InstantiateAsync(_reference, parent);
            _instances.Add(instance);

            return instance;
        }

        public void ReleaseInstance(GameObject instance)
        {
            if (_instances.Remove(instance))
                Addressables.ReleaseInstance(instance);
        }

        public override void ReleaseContainer()
        {
            if (_instances.Any())
                foreach (var go in _instances)
                    Addressables.ReleaseInstance(go);
            _reference.ReleaseAsset();
        }
    }
}