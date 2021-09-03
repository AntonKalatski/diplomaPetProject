using System;
using AnimatorScripts.Readers;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomDeathTransition : StateMachineBehaviour
{
    [SerializeField, Range(0, 1f)] private float deathBackwardsChance;
    [SerializeField, Range(0, 1f)] private float deathForwardChance;
    private static readonly int PlayerDeathForward = Animator.StringToHash("Player_Death_Forward");
    private static readonly int PlayerDeathBackwards = Animator.StringToHash("Player_Death_Backwards");
    private IAnimatorStateReader stateReader;

    private void OnValidate()
    {
        deathBackwardsChance = 1 - deathForwardChance;
        deathForwardChance = 1 - deathBackwardsChance;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var rnd = Random.Range(0, 1f);
        int animState = rnd >= deathBackwardsChance ? PlayerDeathBackwards : PlayerDeathForward;
        animator.SetTrigger(animState);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
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