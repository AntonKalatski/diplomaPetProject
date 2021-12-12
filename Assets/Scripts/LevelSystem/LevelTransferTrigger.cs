using GameSM;
using GameSM.States;
using Services.GameServiceLocator;
using UnityEngine;

namespace LevelSystem
{
    [RequireComponent(typeof(BoxCollider))]
    public class LevelTransferTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider collider;
        [SerializeField] private string transferTo;
        [SerializeField] private bool triggered = false;

        private IGameStateMachine _stateMachine;
        public string TransferTo => transferTo;

        private void Awake()
        {
            collider = GetComponent<BoxCollider>();
            _stateMachine = ServiceLocator.Container.LocateService<IGameStateMachine>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (triggered)
                return;
            triggered = true;
            _stateMachine.Enter<LoadLevelState, string>(transferTo);
        }

        private void OnDrawGizmos()
        {
            if (!ReferenceEquals(collider, null))
            {
                Gizmos.color = new Color32(200, 30, 30, 130);
                Gizmos.DrawCube(transform.position + collider.center, collider.size);
            }
        }
    }
}