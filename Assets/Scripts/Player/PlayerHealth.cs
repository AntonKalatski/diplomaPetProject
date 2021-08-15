using GameData;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerAnimation))]
    public class PlayerHealth : MonoBehaviour, IProgressSaveable
    {
        [SerializeField] private PlayerAnimation anim;
        private PlayerState playerState;

        public float CurrentHp
        {
            get => playerState.currentHp;
            set => playerState.currentHp = value;
        }

        public float MaxHp
        {
            get => playerState.maxHp;
            set => playerState.maxHp = value;
        }

        public void LoadProgress(PlayerProgressData progressData) => playerState = progressData.playerState;
        public void SaveProgress(PlayerProgressData progressData) => progressData.playerState = playerState;

        public void TakeDamage(float damage)
        {
            if (CurrentHp <= 0)
                return;
            CurrentHp -= damage;
            anim.Damage();
        }
    }
}