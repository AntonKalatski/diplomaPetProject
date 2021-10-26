using System.Collections;
using GameData;
using UnityEngine;

namespace Behaviours.Loot
{
    public class LootBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject pickUpFxPrefab;
        [SerializeField] private GameObject pickUpView;
        [SerializeField] private bool isPicked;
        private PlayerProgressData playerProgressData;
        private LootItem lootItem;

        public void Construct(PlayerProgressData data) => playerProgressData = data;

        public void Initialize(LootItem lootItem) => this.lootItem = lootItem;

        private void OnTriggerEnter(Collider other) => PickUp();

        private void PickUp()
        {
            if (isPicked)
                return;
            isPicked = true;
            playerProgressData.lootData.AddLootItem(lootItem);
            pickUpFxPrefab.SetActive(false);
            pickUpView.SetActive(false);
            StartCoroutine(StartDestroyRoutine());
        }

        private IEnumerator StartDestroyRoutine()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }
    }
}