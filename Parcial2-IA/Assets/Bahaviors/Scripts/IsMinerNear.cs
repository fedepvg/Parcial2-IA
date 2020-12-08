using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

[Condition("IsMinerNear")]
public class IsMinerNear : ConditionBase
{
    [InParam("ThisEnemy")]
    public GameObject thisGO;

    public override bool Check()
    {
        if(thisGO.GetComponent<Enemy>().isMinerNear)
           return true;

        return false;
    }
}