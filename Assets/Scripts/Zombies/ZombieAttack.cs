using System.Collections;
using System.Linq;
using Editor;
using GameElements.Health;
using Player;
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
        [SerializeField, Range(0, 3)] private float attackRadius = 1.5f;
        [SerializeField, Range(0, 3)] private float attackHeight = 1.5f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private Collider[] hits = new Collider[1];
        [SerializeField] private Transform survTransform;
        [SerializeField] private bool isAttacking;
        [SerializeField] private float temp;
        private IPlayerGOService playerService;
        private IHealth playerHealth;
        private int layerMask;
        private bool isAttackActive;

        public void OnAttackEnd()
        {
            temp = attackCoolDown;
            isAttacking = false;
            CheckPlayerHealth();
        }


        public void OnAttack()
        {
            if (!ZombieHit(out Collider hit)) return;
            if (hit.transform.TryGetComponent(out playerHealth))
                playerHealth.TakeDamage(damage);
            PhysicsDebug.DrawDebug(hit.transform.position, attackRadius, attackCoolDown, Color.green);
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
            int hitsCount = Physics.OverlapSphereNonAlloc(GetHitPos(), radius: attackRadius, results: hits,
                layerMask: layerMask);
            hit = hits.FirstOrDefault();
            return hitsCount > 0;
        }

        private void OnEnable() => temp = 0;

        private Vector3 GetHitPos()
        {
            var hit = new Vector3(transform.position.x, transform.position.y + attackHeight, transform.position.z) +
                      transform.forward * attackRadius;
            PhysicsDebug.DrawDebug(hit, attackRadius, attackCoolDown, Color.red);
            return hit;
        }


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


        private void CheckPlayerHealth()
        {
            if (playerHealth.CurrentHealth > 0)
                return;
            StartCoroutine(EatingPlayerRoutine());
        }

        private IEnumerator EatingPlayerRoutine()
        {
            transform.GetComponent<ZombieAttack>().enabled = false;
            yield return new WaitForSeconds(1);
            transform.LookAt(survTransform);
            anim.Eating();
        }
    }
}