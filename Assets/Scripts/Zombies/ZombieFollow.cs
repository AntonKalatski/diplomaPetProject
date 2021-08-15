using Services.GameServiceLocator;
using Services.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Zombies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ZombieFollow : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float minDest;
        private IPlayerGOService playerService;
        private Transform survTransform;
        private void Awake()
        {
            agent.stoppingDistance = minDest;
            playerService = ServiceLocator.Container.LocateService<IPlayerGOService>();
            playerService.AddPlayerGORefreshListener(InitializeSurvivorTransform);
        }

        private void Update()
        {
            if (IsSurvivorInitialized() && DestinationToHero())
                agent.destination = survTransform.position;
        }

        private void InitializeSurvivorTransform(GameObject player)
        {
            survTransform = player.transform;
            playerService.RemovePlayerGORefreshListener(InitializeSurvivorTransform);
        }

        private bool IsSurvivorInitialized() => !ReferenceEquals(survTransform, null);

        private bool DestinationToHero() =>
            Vector3.Distance(agent.transform.position, survTransform.position) > minDest;
    }
}