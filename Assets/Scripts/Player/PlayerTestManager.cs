using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerAnimation))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerTestManager : MonoBehaviour
    {
        [Header("Test parameters for attack components")] [SerializeField]
        private float attackRadius;

        [SerializeField] private float attackSpeed;
        [SerializeField] private float attackDamage;

        [Header("Player attack components")] [SerializeField]
        private CharacterController charCont;

        [SerializeField] private PlayerAttack attack;
        [SerializeField] private PlayerAttackZone attackZone;

        private void Start()
        {
            attack.SetAttackProperties(radius: attackRadius, speed: attackSpeed, damage: attackDamage);
            attackZone.SetAttackZone(attackRadius, charCont.center.y / 2);
        }
    }
}