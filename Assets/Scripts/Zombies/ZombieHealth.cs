using System;
using GameElements.Health;
using UI.Actors;
using UnityEngine;

namespace Zombies
{
    public class ZombieHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private ZombieAnimator zombieAnimator;
        [SerializeField] private float currentHealth;
        [SerializeField] private float maxHealth;
        private Action onHealthChanged;
        public void AddOnHealthChangeListener(Action listener) => onHealthChanged += listener;
        public void RemoveOnHealthChangeListener(Action listener) => onHealthChanged -= listener;

        public float CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = value;
        }

        public float MaxHealth
        {
            get => maxHealth;
            set => maxHealth = value;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            zombieAnimator.TakeDamage();
            onHealthChanged?.Invoke();
        }
    }
}