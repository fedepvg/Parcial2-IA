using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using BBUnity.Actions;

[Action("MyActions/ChaseMiner")]
public class Chase : GOAction
{
    public GameObject target;

    Enemy pathfindingAgent;

    public override void OnStart()
    {
        pathfindingAgent = gameObject.GetComponent<Enemy>();
        target = pathfindingAgent.target;
        pathfindingAgent.FindPath(target.transform.position);
    }

    public override TaskStatus OnUpdate()
    {
        if (!pathfindingAgent.pathExists)
            return TaskStatus.COMPLETED;

        pathfindingAgent.FindPath(target.transform.position);
        pathfindingAgent.FollowPath();

        return TaskStatus.RUNNING;
    }
}