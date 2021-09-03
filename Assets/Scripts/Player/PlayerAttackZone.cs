using System;
using UnityEngine;

namespace Player
{
    public class PlayerAttackZone : MonoBehaviour
    {
        [SerializeField] private SphereCollider collider;
        private Action<Collider> triggerEnterListener; //develop into several enemies system List<Collider> enemiess
        private Action triggerExitListeners;
        public void AddOnTriggerEnterListener(Action<Collider> listener) => triggerEnterListener += listener;
        public void RemoveOnTriggerEnterListener(Action<Collider> listener) => triggerEnterListener -= listener;

        public void AddOnTriggerExitListener(Action listener) => triggerExitListeners += listener;
        public void RemoveOnTriggerExitListener(Action listener) => triggerExitListeners -= listener;

        private void OnTriggerEnter(Collider other) => triggerEnterListener?.Invoke(other);
        private void OnTriggerExit(Collider other) => triggerExitListeners?.Invoke();

        public void SetAttackZone(float radius, float center)
        {
            collider.radius = radius;
            collider.center = new Vector3(0, center, 0);
        }

        public void PlayerDeath()
        {
            collider.enabled = false;
        }
    }
}