using UnityEngine;

namespace Player
{
    public class PlayerCheckAttack : MonoBehaviour
    {
        [SerializeField] private PlayerAttackZone attackZone;
        [SerializeField] private PlayerAttack attack;

        private void Awake()
        {
            attackZone.AddOnTriggerEnterListener(TriggerEnterHandler);
            attackZone.AddOnTriggerExitListener(TriggerExitHandler);
            SwitchPlayerAttack(false);
        }

        private void TriggerEnterHandler(Collider enemy) => SwitchPlayerAttack(true, enemy);
        private void TriggerExitHandler() => SwitchPlayerAttack(false);

        private void SwitchPlayerAttack(bool canAttack, Collider enemy = null)
        {
            attack.ChangeActiveEnemy(enemy);
            attack.enabled = canAttack;
        }
    }
}