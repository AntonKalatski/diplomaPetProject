using System;
using UnityEngine;

namespace Bootstrap
{
    public class SceneRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrap bootstrapPrefab;

        private void Awake()
        {
            if (ReferenceEquals(FindObjectOfType<GameBootstrap>(), null))
                Instantiate(bootstrapPrefab);
        }
    }
}