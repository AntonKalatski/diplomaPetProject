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
        private IPlayerGOService playerGO;
        private Transform survTransform;

        private void Awake() => agent ??= GetComponent<NavMeshAgent>();

        private void Start()
        {
            playerGO = ServiceLocator.Container.LocateService<IPlayerGOService>();
            if (!ReferenceEquals(playerGO.GetPlayerGameObject(), null))
                InitializeSurvivorTransform();
        }

        private void InitializeSurvivorTransform() => survTransform = playerGO.GetPlayerTransform();

        private void Update()
        {
            if (IsSurvivorInitialized() && DestinationToHero())
                agent.destination = survTransform.position;
        }

        private bool IsSurvivorInitialized() => !ReferenceEquals(survTransform, null);

        private bool DestinationToHero() =>
            Vector3.Distance(agent.transform.position, survTransform.position) > minDest;
    }
}