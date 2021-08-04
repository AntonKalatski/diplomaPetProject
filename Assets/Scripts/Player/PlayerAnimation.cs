using System;
using Services;
using Services.GameServiceLocator;
using UnityEditor.Animations;
using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private static readonly int SpeedF = Animator.StringToHash("Speed_f");
        private IInputService inputService;

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }
        public Animator Animator => animator;

        private void Awake()
        {
            inputService = ServiceLocator.Container.LocateService<IInputService>();
        }

        private void Update()
        {
            animator.SetFloat(SpeedF,inputService.Axis.magnitude);
        }
    }
}