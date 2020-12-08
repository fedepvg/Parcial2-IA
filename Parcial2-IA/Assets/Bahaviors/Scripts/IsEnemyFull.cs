using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

[Condition("IsEnemyFull")]
public class IsEnemyFull : ConditionBase
{
    [InParam("ThisEnemy")]
    public GameObject thisGO;

    [InParam("Condition")]
    public bool condition;

    public override bool Check()
    {
        if (thisGO.GetComponent<Enemy>().IsFull() == condition)
            return true;

        return false;
    }
}