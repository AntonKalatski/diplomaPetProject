using Triggers.Observer;
using UnityEngine;

namespace Zombies
{
    [RequireComponent(typeof(ZombieAttack))]
    public class ZombieCheckAttack : MonoBehaviour
    {
        [SerializeField] private ZombieAttack attack;
        [SerializeField] private TriggerObserver triggerObserver;

        private void Awake()
        {
            triggerObserver.AddOnTriggerEnterListener(TriggerEnterHandler);
            triggerObserver.AddOnTriggerExitListener(TriggerExitHandler);
            SwitchZombieAttack(false);
        }

        private void TriggerExitHandler(Collider obj) => SwitchZombieAttack(false);
        private void TriggerEnterHandler(Collider obj) => SwitchZombieAttack(true);
        private void SwitchZombieAttack(bool canAttack)
        {
            attack.enabled = canAttack;
        }
    }
}