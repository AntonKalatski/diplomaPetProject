using System;
using AnimatorScripts.Readers;
using AnimatorScripts.States;
using UnityEngine;

namespace Zombies
{
    [RequireComponent(typeof(Animator))]
    public class ZombieAnimator : MonoBehaviour, IAnimatorStateReader
    {
        [SerializeField] private Animator animator;

        private static readonly int ZombieAttack = Animator.StringToHash("zombie_attack");
        private static readonly int ZombieIdle = Animator.StringToHash("zombie_idle");
        private static readonly int ZombieDie = Animator.StringToHash("zombie_die");
        private static readonly int ZombieEating = Animator.StringToHash("zombie_eating");
        private static readonly int ZombieIsMoving = Animator.StringToHash("zombie_is_moving");
        private static readonly int ZombieSpeed = Animator.StringToHash("zombie_move_speed");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        public AnimatorState State { get; private set; }

        public void Death() => animator.SetTrigger(ZombieDie);
        public void Eating() => animator.SetTrigger(ZombieEating);
        public void Attack() => animator.SetTrigger(ZombieAttack);

        public void TakeDamage()//todo make damage for zomibe
        {
            Debug.Log("Zombie taking damage");
        }
        public void StopMoving() => animator.SetBool(ZombieIsMoving, false);

        public void Move(float speed)
        {
            animator.SetBool(ZombieIsMoving, true);
            animator.SetFloat(ZombieSpeed, speed);
        }

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) => StateExited?.Invoke(StateFor(stateHash));

        public AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;

            if (stateHash.Equals(ZombieIdle))
                state = AnimatorState.ZombieIdle;
            else if (stateHash.Equals(ZombieDie))
                state = AnimatorState.ZombieDie;
            else if (stateHash.Equals(ZombieEating))
                state = AnimatorState.ZombieEating;
            else if (stateHash.Equals(ZombieIsMoving))
                state = AnimatorState.ZombieEating;
            else
                state = AnimatorState.Unknown;
            return state;
        }

    }
}