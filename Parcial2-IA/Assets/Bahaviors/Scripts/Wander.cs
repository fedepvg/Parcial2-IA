using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

[Action("MyActions/Wander")]
public class Wander : BBUnity.Actions.GOAction
{
    PathfindingAgent pathfindingAgent;

    public override void OnStart()
    {
        pathfindingAgent = gameObject.GetComponent<PathfindingAgent>();
        pathfindingAgent.GetPathToRandomLocation();
    }

    public override TaskStatus OnUpdate()
    {
        if (!pathfindingAgent.pathExists)
            return TaskStatus.COMPLETED;

        pathfindingAgent.FollowPath();

        return TaskStatus.RUNNING;
    }
}