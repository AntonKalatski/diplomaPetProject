using System;
using Editor;
using GameData;
using GameElements.Health;
using Services;
using Services.GameServiceLocator;
using UnityEngine;
using Zombies;

namespace Player
{
    public class PlayerAttack : MonoBehaviour, IProgressSaveable, IInputListener
    {
        [SerializeField] private PlayerAnimation anim;
        [SerializeField] private CharacterController charCont;
        [SerializeField] private PlayerMovement move;
        [SerializeField] private float attackRadius = 1.5f; //depends on weapon
        [SerializeField] private float attackCoolDown = 1.5f; //depends on weapon
        [SerializeField] private float attackDamage = 10f; //depends on weapon

        //remove after test
        [SerializeField] private Transform activeEnemy;
        [SerializeField] private bool isAttacking;
        [SerializeField] private float temp;
        private int layerMask;
        private Collider[] hits = new Collider[3];
        private ZombieDeath zombie1;
        private IInputService inputService;

        public void OnPlayerAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                hits[i].transform.TryGetComponent<IHealth>(out var enemyHealth);
                enemyHealth.TakeDamage(attackDamage);
                PhysicsDebug.DrawDebug(hits[i].transform.position, attackRadius, attackCoolDown, Color.yellow);
            }
        }

        public void OnPlayerAttackEnd()
        {
            Debug.Log("PlayerAttackEnd");
            temp = attackCoolDown;
            isAttacking = false;
        }

        public void ChangeActiveEnemy(Collider enemy)
        {
            if (enemy == null)
                return;
            zombie1 = enemy.GetComponent<ZombieDeath>();
            zombie1.AddOnDeathListener(ZombieDeathListener);
            activeEnemy = zombie1.transform;
        }

        private void ZombieDeathListener()
        {
            zombie1.RemoveOnDeathListener(ZombieDeathListener);
            transform.GetComponent<PlayerAttack>().enabled = false;
        }

        public void SetAttackProperties(float radius, float speed, float damage)
        {
            attackRadius = radius;
            attackCoolDown = speed;
            attackDamage = damage;
        }

        public void LoadProgress(PlayerProgressData progressData)
        {
            // todo load gun if exists
            // attackRadius = progressData.playerStats.DamageRadius;
        }

        public void SaveProgress(PlayerProgressData progressData)
        {
        }

        private void Awake()
        {
            layerMask = 1 << LayerMask.NameToLayer("Enemy");
            inputService = ServiceLocator.Container.LocateService<IInputService>();
            inputService.AddInputListener(this);
        }

        private void OnDestroy()
        {
            inputService.RemoveInputListener(this);
        }

        public void AttackButton()
        {
            if (!isAttacking)
                Attack();
        }

        private void OnEnable() => temp = 0;

        private bool CanAttack() => !isAttacking;

        private bool CooldownEnd() => temp <= 0;

        private int Hit()
        {
            return Physics.OverlapSphereNonAlloc(GetHitPos() + transform.forward, radius: attackRadius, hits,
                layerMask);
        }

        private void Attack()
        {
            Debug.Log("Attack really");
            transform.LookAt(activeEnemy);
            anim.Attack();
            isAttacking = true;
        }

        private Vector3 GetHitPos() => new Vector3(transform.position.x, charCont.center.y / 2, transform.position.z);
    }
}