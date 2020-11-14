using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBehaviour : StateMachineBehaviour
{
    Miner owner;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        owner = animator.GetComponent<Miner>();

        owner.FindPath(owner.baseRef.transform.position, ()=>
        {
            animator.SetTrigger("reachedBase");
            GoldManager.Instance.AddGold(owner.DropGold());
        });
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (owner.pathExists)
            owner.FollowPath();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
