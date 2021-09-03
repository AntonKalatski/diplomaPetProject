using System;
using GameData;
using GameElements.Health;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerAnimation))]
    [RequireComponent(typeof(PlayerVfx))]
    public class PlayerHealth : MonoBehaviour, IProgressSaveable, IHealth
    {
        [SerializeField] private PlayerAnimation anim;
        [SerializeField] private PlayerVfx vfx;
        private PlayerState playerState;
        private Action onHealthChange;

        public float CurrentHealth
        {
            get => playerState.currentHp;
            set
            {
                if (!(Math.Abs(playerState.currentHp - value) > 0.1f)) return;
                playerState.currentHp = value;
                onHealthChange?.Invoke();
            }
        }

        public float MaxHealth
        {
            get => playerState.maxHp;
            set => playerState.maxHp = value;
        }

        public void LoadProgress(PlayerProgressData progressData) => playerState = progressData.playerState;

        public void SaveProgress(PlayerProgressData progressData) => progressData.playerState = playerState;

        public void AddOnHealthChangeListener(Action listener) => onHealthChange += listener;
        public void RemoveOnHealthChangeListener(Action listener) => onHealthChange -= listener;

        public void TakeDamage(float damage)
        {
            if (CurrentHealth <= 0)
                return;
            CurrentHealth -= damage;
            anim.Damage();
            vfx.Damage();
            vfx.Damage();
        }
    }
}