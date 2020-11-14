using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkingBehaviour : StateMachineBehaviour
{
    Explorer owner;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        owner = animator.GetComponent<Explorer>();

        owner.currentPath.onEndPathDelegate += MarkMine;
        owner.currentPath.onEndPathDelegate += () =>
        {
            animator.SetTrigger("markedMine");
        };
    }

    public void MarkMine()
    {
        owner.PutFlag();
        owner.pathExists = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if(owner.pathExists)
            owner.FollowPath();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
