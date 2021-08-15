using Services.GameServiceLocator;
using Services.SaveLoad;
using UnityEngine;

namespace Triggers.Save
{
    [RequireComponent(typeof(BoxCollider))]
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider coll;
        private ISaveLoadService saveLoadService;

        private void Awake()
        {
            coll ??= GetComponent<BoxCollider>();
            saveLoadService = ServiceLocator.Container.LocateService<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            saveLoadService.SaveProgress();
            Debug.Log("Progress saved");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (ReferenceEquals(coll, null))
                return;
            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + coll.center, coll.size);
        }
    }
}