using System;
using Effects;
using GameData;
using UnityEngine;

namespace Zombies
{
    public class ZombieDeath : MonoBehaviour
    {
        [SerializeField] private ZombieBodyProvider zombieBodyProvider;
        [SerializeField] private ZombieAnimator zombieAnimator;
        [SerializeField] private ZombieHealth zombieHealth;
        [SerializeField] private ZombieFollow zombieFollow;
        [SerializeField] private BloodPuddleEffect bloodPuddle;
        private PlayerProgressData playerProgressData;
        private Action onZombieDeath;

        public void OnDeathEnd()
        {
            SpawnBloodBuddle();
        }

        public void AddOnDeathListener(Action listener) => onZombieDeath += listener;

        public void RemoveOnDeathListener(Action listener) => onZombieDeath -= listener;

        public void Initialize(PlayerProgressData data) => playerProgressData = data;

        private void Awake() => zombieHealth.AddOnHealthChangeListener(ZombieHealthChangeHandler);

        private void ZombieHealthChangeHandler()
        {
            if (zombieHealth.CurrentHealth <= 0)
                ZombieDie();
        }

        private void ZombieDie()
        {
            zombieHealth.RemoveOnHealthChangeListener(ZombieHealthChangeHandler);
            playerProgressData.killData.ZombieKilled();
            zombieAnimator.Death();
            zombieFollow.enabled = false;
            onZombieDeath?.Invoke();
        }

        private void SpawnBloodBuddle() =>
            Instantiate(bloodPuddle, transform).ShowBloodPuddle(zombieBodyProvider.Hips);

        private void OnDestroy() => zombieHealth.RemoveOnHealthChangeListener(ZombieHealthChangeHandler);
    }
}