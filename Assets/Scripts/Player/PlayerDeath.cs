using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerVfx))]
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private PlayerBodyProvider bodyProvider;
        [SerializeField] private PlayerHealth health;
        [SerializeField] private PlayerAttack attack;
        [SerializeField] private PlayerAttackZone attackZone;
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerAnimation animator;
        [SerializeField] private PlayerVfx vfx;
        private bool isDead;

        public void OnDeathEnd() => vfx.Death();
        private void Start() => health.AddOnHealthChangeListener(HealthChangeHandler);
        private void OnDestroy() => health.RemoveOnHealthChangeListener(HealthChangeHandler);

        private void HealthChangeHandler()
        {
            if (!isDead && health.CurrentHealth <= 0)
                PlayerDie();
        }

        private void PlayerDie()
        {
            isDead = true;
            animator.Death();
            movement.enabled = false;
            attack.enabled = false;
            attackZone.PlayerDeath();
        }
    }
}