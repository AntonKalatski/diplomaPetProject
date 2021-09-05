using System.Collections.Generic;
using Factories.Interfaces;
using Player;
using Providers.Assets;
using UnityEngine;

namespace Factories
{
    public abstract class AbstractGameFactory : IGameFactory
    {
        private readonly IAssetProvider assetProvider;
        public List<IProgressLoadable> ProgressLoadables { get; } = new List<IProgressLoadable>();
        public List<IProgressSaveable> ProgressSaveables { get; } = new List<IProgressSaveable>();

        protected AbstractGameFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public void Register(GameObject gameObject) => RegisterProgressLoaders(gameObject);

        public void CleanUp()
        {
            ProgressLoadables.Clear();
            ProgressSaveables.Clear();
        }

        protected GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = assetProvider.Instantiate(prefabPath, position: at);
            RegisterProgressLoaders(gameObject);
            return gameObject;
        }

        protected GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = assetProvider.Instantiate(prefabPath);
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