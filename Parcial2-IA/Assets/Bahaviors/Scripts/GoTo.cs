using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using BBUnity.Actions;

[Action("MyActions/GoTo")]
public class GoTo : GOAction
{
    [InParam("TargetObj")]
    public GameObject target;

    Enemy pathfindingAgent;

    public override void OnStart()
    {
        pathfindingAgent = gameObject.GetComponent<Enemy>();
        pathfindingAgent.FindPath(target.transform.position);
    }

    public override TaskStatus OnUpdate()
    {
        if (!pathfindingAgent.pathExists)
            return TaskStatus.COMPLETED;

        pathfindingAgent.FollowPath();

        return TaskStatus.RUNNING;
    }
}