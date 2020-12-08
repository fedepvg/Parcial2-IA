using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

[Condition("IsMinerNear")]
public class IsMinerNear : ConditionBase
{
    [InParam("ThisEnemmy")]
    public GameObject thisGO;

    [InParam("Miner")]
    public GameObject MinerGO;


    public override bool Check()
    {
        if(thisGO.GetComponent<Enemy>().isMinerNear)
           return true;

        return false;
    }
}
