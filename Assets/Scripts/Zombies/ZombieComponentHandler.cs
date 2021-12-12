using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Zombies
{
    public class ZombieComponentHandler : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> components;
        [SerializeField] private List<Collider> colliders;
        [SerializeField] private List<Collider> headColliders;
        [SerializeField] private ZombieDeath deathComponent;

        private void Awake()
        {
            headColliders = transform.GetComponents<Collider>().ToList();
            colliders = transform.GetComponentsInChildren<Collider>().ToList();
            components = transform.GetComponents<MonoBehaviour>().ToList();

            if (transform.TryGetComponent<ZombieDeath>(out var zombieDeath))
            {
                deathComponent = zombieDeath;
            }


            deathComponent.AddOnDeathListener(ZombieDeathHandler);
        }


        private void ZombieDeathHandler()
        {
            foreach (var component in components)
            {
                component.enabled = false;
            }

            headColliders.ForEach(x => x.enabled = false);
            colliders.ForEach(x => x.enabled = false);
            StartCoroutine(DeathCountDownRoutine());
        }

        public IEnumerator DeathCountDownRoutine()
        {
            yield return new WaitForSeconds(5f);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            deathComponent.RemoveOnDeathListener(ZombieDeathHandler);
        }
    }
}