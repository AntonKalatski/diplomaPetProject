using System.Collections.Generic;
using System.Threading.Tasks;
using Factories.Interfaces;
using Player;
using Providers.Assets;
using UnityEngine;

namespace Factories
{
    public abstract class AbstractGameFactory : IGameFactory
    {
        protected readonly IAssetProvider assetProvider;
        public List<IProgressLoadable> ProgressLoadables { get; } = new List<IProgressLoadable>();
        public List<IProgressSaveable> ProgressSaveables { get; } = new List<IProgressSaveable>();

        protected AbstractGameFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        protected void Register(GameObject gameObject)
        {
            RegisterProgressLoaders(gameObject);
        }

        public virtual void CleanUp()
        {
            ProgressLoadables.Clear();
            ProgressSaveables.Clear();
        }
        protected async Task<GameObject> Instantiate(string prefabPath)
        {
            return await assetProvider.Instantiate(prefabPath);
        }

        protected async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
        {
            GameObject gameObject = await assetProvider.Instantiate(prefabPath);
            RegisterProgressLoaders(gameObject);
            return gameObject;
        }

        protected async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 at)
        {
            GameObject gameObject = await assetProvider.Instantiate(prefabPath, position: at);
            RegisterProgressLoaders(gameObject);
            return gameObject;
        }

        protected GameObject InstantiateRegisteredAsync(GameObject prefab, Vector3 at)
        {
            GameObject gameObject = Object.Instantiate(prefab,at,Quaternion.identity);
            RegisterProgressLoaders(gameObject);
            return gameObject;
        }

        private void RegisterProgressLoaders(GameObject gameObject)
        {
            foreach (IProgressLoadable progressLoadable in gameObject.GetComponentsInChildren<IProgressLoadable>())
                RegisterLoader(progressLoadable);
        }

        private void RegisterLoader(IProgressLoadable progressLoadable)
        {
            if (progressLoadable is IProgressSaveable progressSaveable)
                ProgressSaveables.Add(progressSaveable);

            ProgressLoadables.Add(progressLoadable);
        }
    }
}