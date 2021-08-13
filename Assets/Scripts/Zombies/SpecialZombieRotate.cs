using Factories.Interfaces;
using Services.GameServiceLocator;
using Services.Player;
using UnityEngine;

namespace Zombies
{
    public class SpecialZombieRotate : MonoBehaviour
    {
        [SerializeField] private float minDest;
        [SerializeField] private float speed;
        private IPlayerGOService playerGO;

        private Transform survTransform;
        private Vector3 lookDirection;

        private void Start()
        {
            playerGO = ServiceLocator.Container.LocateService<IPlayerGOService>();
            if (!ReferenceEquals(playerGO.GetPlayerTransform(), null))
                InitializeSurvivorTransform();
        }

        private void InitializeSurvivorTransform() => survTransform = playerGO.GetPlayerTransform();

        private void Update()
        {
            if (IsInitialized())
                RotateTowards();
        }

        private void RotateTowards()
        {
            UpdateDirection();
            transform.rotation = SmoothRotation(transform.rotation, lookDirection);
        }

        private void UpdateDirection()
        {
            Vector3 delta = survTransform.position - transform.position;
            lookDirection = new Vector3(delta.x, transform.position.y, delta.z);
        }

        private Quaternion SmoothRotation(Quaternion rotation, Vector3 direction) => Quaternion.Lerp(rotation, Quaternion.LookRotation(direction), speed * Time.deltaTime);
        private bool IsInitialized() => IsSurvivorInitialized() && DestinationToHero();
        private bool IsSurvivorInitialized() => !ReferenceEquals(survTransform, null);
        private bool DestinationToHero() => Vector3.Distance(transform.position, survTransform.position) > minDest;
    }
}