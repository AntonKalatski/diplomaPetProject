using UnityEngine;
using System.Collections.Generic;
using Services.GameServiceLocator;

namespace Managers
{
    public class TickableManager : MonoBehaviour, ITickableManager, IService
    {
        private HashSet<ITickable> tickables;

        private void Awake()
        {
            tickables = new HashSet<ITickable>();
            DontDestroyOnLoad(gameObject);
            Debug.LogWarning("Tickable Manager started to work");
        }

        private void Update()
        {
            foreach (var tickable in tickables)
                tickable.Tick();
        }

        public void AddTickable(ITickable tickable)
        {
            if (tickables.Add(tickable))
                Debug.LogWarning($"Tickable Manager successfully added {tickable.GetType()}");
        }

        public void RemoveTickable(ITickable tickable)
        {
            if (tickables.Remove(tickable))
                Debug.LogWarning($"Tickable Manager successfully removed {tickable.GetType()}");
        }
    }
}