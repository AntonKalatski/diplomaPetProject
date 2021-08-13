using System.Collections;
using Triggers.Observer;
using UnityEngine;

namespace Zombies
{
    public class ZombieAggression : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private ZombieFollow move;

        [SerializeField, Range(0f, 5f)] private float coolDown;

        //todo remove after test
        [SerializeField] private bool hasTarget;
        private Coroutine activeCoroutine;

        private void Awake()
        {
            triggerObserver.AddOnTriggerEnterListener(TriggerEnterHandler);
            triggerObserver.AddOnTriggerExitListener(TriggerExitHandler);
            SwitchZombieFollow(false);
        }

        private void TriggerEnterHandler(Collider collider)
        {
            if (hasTarget) return;
            hasTarget = true;
            StopCoroutine();
            SwitchZombieFollow(true);
        }

        private void TriggerExitHandler(Collider collider)
        {
            if (!hasTarget) return;
            hasTarget = false;
            activeCoroutine = StartCoroutine(FollowCoolDownRoutine());
        }

        private void StopCoroutine()
        {
            if (ReferenceEquals(activeCoroutine, null))
                return;
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }

        private IEnumerator FollowCoolDownRoutine()
        {
            yield return new WaitForSeconds(coolDown);
            SwitchZombieFollow(false);
        }

        private void SwitchZombieFollow(bool isOn) => move.enabled = isOn;
    }
}