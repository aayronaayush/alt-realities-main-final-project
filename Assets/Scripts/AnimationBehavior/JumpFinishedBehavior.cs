using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFinishedBehavior : StateMachineBehaviour
{
    int isDancingHash;

    private void Awake()
    {
        isDancingHash = Animator.StringToHash("isDancing");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(isDancingHash, false);
    }
}

