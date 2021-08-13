using UnityEngine;
using UnityEngine.AI;

namespace Zombies
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(ZombieAnimator))]
    public class ZombieAnimateAlongAgent : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private ZombieAnimator anim;
        [SerializeField] private float minVelocity = 0.1f;

        private void Awake()
        {
            agent ??= GetComponent<NavMeshAgent>();
            anim ??= GetComponent<ZombieAnimator>();
        }

        private void Update()
        {
            if (CanMove())
                anim.Move(agent.velocity.magnitude);
            else
                anim.StopMoving();
        }

        private bool CanMove() => agent.velocity.magnitude > minVelocity && agent.remainingDistance > agent.radius;
    }
}