using System;
using AnimatorScripts.Readers;
using AnimatorScripts.States;
using Services;
using Services.GameServiceLocator;
using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour, IAnimatorStateReader
    {
        [SerializeField] private Animator animator;

        private static readonly int SpeedF = Animator.StringToHash("Speed_f");
        private static readonly int PlayerDeath = Animator.StringToHash("Player_Death");
        private static readonly int PlayerMeleeTwoHanded = Animator.StringToHash("Player_Melee_Two_Handed");
        private IInputService inputService;
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }
        public Animator Animator => animator;
        public bool IsAttacking { get; set; }

        private void Awake() => inputService = ServiceLocator.Container.LocateService<IInputService>();
        private void Update() => animator.SetFloat(SpeedF, inputService.Axis.magnitude);
        public void Damage() => Debug.Log("Playing damage anim");
        public void Death() => animator.SetBool(PlayerDeath, true);
        public void Attack() => animator.SetTrigger(PlayerMeleeTwoHanded);

        public void EnteredState(int stateHash)
        {
        }

        public void ExitedState(int stateHash)
        {
        }

        public AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            state = AnimatorState.PlayerIdle;
            // if (stateHash.Equals(ZombieIdle))
            //     state = AnimatorState.ZombieIdle;
            // else if (stateHash.Equals(ZombieDie))
            //     state = AnimatorState.ZombieDie;
            // else if (stateHash.Equals(ZombieEating))
            //     state = AnimatorState.ZombieEating;
            // else if (stateHash.Equals(ZombieIsMoving))
            //     state = AnimatorState.ZombieEating;
            // else
            //     state = AnimatorState.Unknown;

            return state;
        }
    }
}