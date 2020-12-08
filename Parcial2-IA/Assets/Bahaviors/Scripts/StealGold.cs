using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using BBUnity.Actions;

[Action("MyActions/StealGold")]
public class StealGold : GOAction
{
    public GameObject target;

    Enemy pathfindingAgent;
    Miner miner;

    public override void OnStart()
    {
        pathfindingAgent = gameObject.GetComponent<Enemy>();
        target = pathfindingAgent.target;
        miner = target.GetComponent<Miner>();
    }

    public override TaskStatus OnUpdate()
    {
        miner.Mine(pathfindingAgent.StealGold(miner.DropGold()));
        
        return TaskStatus.COMPLETED;
    }
}