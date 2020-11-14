using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : PathfindingAgent
{
    public GameObject mineFlagPrefab;
    public Mine mineRef;

    override protected void Start()
    {
        base.Start();
        visionCone.onTargetFoundAction += (GameObject target) =>
         {
             mineRef = target.GetComponent<Mine>();
         };
    }

    public void MarkMine()
    {
        Instantiate(mineFlagPrefab, transform.position, transform.rotation, mineRef.transform);
        mineRef.BeginExploration();
    }
}
