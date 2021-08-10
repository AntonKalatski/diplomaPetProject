using UnityEngine;
using Zombies;

namespace AnimatorBehaviors
{
    public class AnimatorStateReporter : StateMachineBehaviour
    {
        private IAnimatorStateReader stateReader;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            FindReader(animator);

            stateReader.ExitedState(stateInfo.shortNameHash);
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            FindReader(animator);

            stateReader.EnteredState(stateInfo.shortNameHash);
        }

        private void FindReader(Animator animator)
        {
            if (!ReferenceEquals(stateReader, null))
                return;
            animator.gameObject.TryGetComponent<IAnimatorStateReader>(out stateReader);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}