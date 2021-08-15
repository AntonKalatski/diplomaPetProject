using AnimatorScripts.Readers;
using UnityEngine;

namespace AnimatorScripts.Reporters
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
    }
}