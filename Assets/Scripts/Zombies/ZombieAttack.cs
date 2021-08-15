using System;
using System.Linq;
using Editor;
using Services.GameServiceLocator;
using Services.Player;
using UnityEngine;

namespace Zombies
{
    [RequireComponent(typeof(ZombieAnimator))]
    public class ZombieAttack : MonoBehaviour
    {
        [SerializeField] private ZombieAnimator anim;
        [SerializeField, Range(0, 5)] private float attackCoolDown = 2f;
        [SerializeField, Range(0, 3)] private float attackRadius = 0.5f;
        [SerializeField, Range(0, 3)] private float attackHeight = 0.5f;
        [SerializeField] private Collider[] hits = new Collider[1];
        [SerializeField] private Transform survTransform;
        [SerializeField] private bool isAttacking;
        [SerializeField] private float temp;
        private IPlayerGOService playerService;
        private int layerMask;
        private bool isAttackActive;

        public void OnAttackEnd()
        {
            temp = attackCoolDown;
            isAttacking = false;
        }

        public void OnAttack()
        {
            if (ZombieHit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(GetHitPos(), attackRadius, attackCoolDown);
            }
        }

        private void Awake()
        {
            layerMask = 1 << LayerMask.NameToLayer("Player");
            playerService = ServiceLocator.Container.LocateService<IPlayerGOService>();
            playerService.AddPlayerGORefreshListener(InitializeSurvivorTransform);
        }

        private void InitializeSurvivorTransform(GameObject player)
        {
            survTransform = player.transform;
            playerService.RemovePlayerGORefreshListener(InitializeSurvivorTransform);
        }

        private bool ZombieHit(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(GetHitPos(), attackRadius, hits, layerMask);
            hit = hits.FirstOrDefault();
            return hitsCount > 0;
        }

        private void OnEnable() => temp = 0;

        private Vector3 GetHitPos() =>
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
            transform.forward * attackRadius;

        private void Update()
        {
            UpdateCooldown();
            if (CanAttack())
                Attack();
        }

        private void UpdateCooldown()
        {
            if (!CooldownEnd())
                temp -= Time.deltaTime;
        }

        private void Attack()
        {
            transform.LookAt(survTransform);
            anim.Attack();
            isAttacking = true;
        }

        private bool CanAttack() => !isAttacking && CooldownEnd();

        private bool CooldownEnd() => temp <= 0;
    }
}