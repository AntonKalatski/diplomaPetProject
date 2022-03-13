using System;
using UnityEngine;

namespace Scenes.Test.animators
{
    public class OverrideAnimatorController : AnimatorOverrideController
    {
       
    }
    public class AnimTest : RuntimeAnimatorController
    {
       
    }

    public class Test : MonoBehaviour
    {
        [SerializeField] private AnimatorOverrideController contr;

        private void Start()
        {
            var clip = contr.animationClips[0];
        }
    }
}
