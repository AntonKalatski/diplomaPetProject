using UnityEngine;

namespace Providers.Assets
{
    public class AssetProvider : IAssetProvider
    {
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