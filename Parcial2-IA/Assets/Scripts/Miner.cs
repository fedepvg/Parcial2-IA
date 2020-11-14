using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : PathfindingAgent
{
    public Mine mineRef;
    public GameObject baseRef;
    public int maxGoldToTake;
    public int goldToTakePerSecond;

    float currentGold;

    override protected void Start()
    {
        base.Start();

    }

    override protected void Update()
    {
        base.Update();
    }

    public void Mine(float minedGold)
    {
        currentGold += minedGold;
    }

    public bool IsFull()
    {
        return currentGold >= maxGoldToTake;
    }

    public int DropGold()
    {
        int gold = (int)currentGold;
        currentGold = 0;
        return gold;
    }
}
