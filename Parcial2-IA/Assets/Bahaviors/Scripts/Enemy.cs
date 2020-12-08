using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PathfindingAgent
{
    public bool isMinerNear = false;
    public GameObject target;
    public int maxGold;


    public delegate void OnEnemyDestroyed();
    public OnEnemyDestroyed onEnemyDestroyed;

    BehaviorExecutor behavior;
    int currentGold = 0;

    override protected void Awake()
    {
        base.Awake();

        behavior = GetComponent<BehaviorExecutor>();
        behavior.SetBehaviorParam("ThisEnemy", this.gameObject);
    }

    override protected void Start()
    {
        base.Start();

        visionCone.onTargetFoundAction = OnMinerFound;
        visionCone.onTargetLost = OnMinerLost;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMinerNear)
        {
            if (target.layer != LayerMask.NameToLayer("FullMiner"))
            {
                isMinerNear = false;
                target = null;
            }
            return;
        }

        visionCone.FindVisibleTargets();
    }

    void OnMinerFound(GameObject target)
    {
        isMinerNear = true;

        this.target = target;
    }

    void OnMinerLost()
    {
        isMinerNear = true;

        this.target = null;
    }

    public int StealGold(int gold)
    {
        currentGold += gold;

        if (currentGold > maxGold)
        {
            int dif = currentGold - maxGold;
            currentGold = maxGold;
            return dif;
        }

        return 0;
    }

    public bool IsFull()
    {
        return currentGold == maxGold;
    }

    private void OnDestroy()
    {
        if (onEnemyDestroyed != null)
            onEnemyDestroyed();
    }
}
