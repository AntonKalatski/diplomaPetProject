using System;
using AnimatorBehaviors;
using UnityEngine;

namespace Zombies
{
    public class ZombieAnimator : MonoBehaviour, IAnimatorStateReader
    {
        [SerializeField] private Animator animator;

        private static readonly int ZombieIdle = Animator.StringToHash("zombie_idle");
        private static readonly int ZombieDie = Animator.StringToHash("zombie_die");
        private static readonly int ZombieEating = Animator.StringToHash("zombie_eating");
        private static readonly int ZombieIsMoving = Animator.StringToHash("zombie_is_moving");
        private static readonly int ZombieSpeed = Animator.StringToHash("zombie_move_speed");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        public AnimatorState State { get; private set; }

        private void Awake()
        {
            if (ReferenceEquals(animator, null))
                animator = GetComponent<Animator>();
        }

        public void Death() => animator.SetTrigger(ZombieDie);
        public void Eating() => animator.SetTrigger(ZombieEating);

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