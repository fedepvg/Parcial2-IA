using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningBehaviour : StateMachineBehaviour
{
    Miner owner;
    bool isMining = false;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        owner = animator.GetComponent<Miner>();

        if (!owner.pathExists)
        {
            owner.FindPath(owner.mineRef.transform.position);
        }

        owner.currentPath.onEndPathDelegate += () =>
        {
            isMining = true;
        };
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (owner.pathExists)
            owner.FollowPath();
        if(isMining)
        {
            owner.Mine(owner.mineRef.TakeGold(owner.goldToTakePerSecond * Time.deltaTime));
            Debug.Log("Mining");
            if (owner.IsFull())
                EndMining(animator);

            if (owner.mineRef.IsEmpty())
            {
                Destroy(owner.mineRef.gameObject);
                EndMining(animator);
            }
        }
    }

    void EndMining(Animator animator)
    {
        animator.SetTrigger("endedMining");
        isMining = false;
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isMining = false;
    }
}
