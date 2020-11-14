using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    PathfindingAgent owner;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        owner = animator.GetComponent<PathfindingAgent>();

        owner.visionCone.onTargetFoundAction += (GameObject target) =>
        {
            animator.SetTrigger("foundMine");
            owner.FindPath(target.transform.position);
            target.GetComponent<Mine>().BeginExploration();
        };

        //owner.GetPathToRandomLocation();
        //PathfindingAgent.endNodeReachedAction = () => { animator.SetTrigger("foundMine"); };
        Debug.Log("patrol entered");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        owner.visionCone.FindVisibleTargets();

        if (!owner.pathExists)
            owner.GetPathToRandomLocation();
        else
            owner.FollowPath();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
