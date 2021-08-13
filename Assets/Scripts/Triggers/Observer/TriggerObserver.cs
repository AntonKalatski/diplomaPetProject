using System;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Observer
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        private List<Action<Collider>> triggerEnterListeners = new List<Action<Collider>>();
        private List<Action<Collider>> triggerExitListeners = new List<Action<Collider>>();

        public void AddOnTriggerEnterListener(Action<Collider> listener) => triggerEnterListeners.Add(listener);
        public void RemoveOnTriggerEnterListener(Action<Collider> listener) => triggerEnterListeners.Remove(listener);

        public void AddOnTriggerExitListener(Action<Collider> listener) => triggerExitListeners.Add(listener);
        public void RemoveOnTriggerExitListener(Action<Collider> listener) => triggerExitListeners.Remove(listener);
        private void OnTriggerEnter(Collider other) => triggerEnterListeners.ForEach(x => x?.Invoke(other));
        private void OnTriggerExit(Collider other) => triggerExitListeners.ForEach(x => x?.Invoke(other));
    }
}