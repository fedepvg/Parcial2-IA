using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PathfindingAgent
{
    public bool isMinerNear = false;
    
    BehaviorExecutor behavior;

    override protected void Start()
    {
        base.Start();

        behavior = GetComponent<BehaviorExecutor>();

        visionCone.onTargetFoundAction = OnMinerFound;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMinerNear)
            return;

        visionCone.FindVisibleTargets();
    }

    void OnMinerFound(GameObject target)
    {
        isMinerNear = true;

        behavior.SetBehaviorParam("Miner", target);
    }
}
