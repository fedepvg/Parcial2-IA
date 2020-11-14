using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public int minGoldAmount;
    public int maxGoldAmount;

    public delegate void OnBeginExploration();
    public OnBeginExploration onBeginExplorationAction;
    public delegate void OnMineDestroy();
    public OnMineDestroy onMineDestroyAction;

    int remainingGold;
    bool isBeingExplored = false;

    // Start is called before the first frame update
    void Start()
    {
        remainingGold = Random.Range(minGoldAmount, maxGoldAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (onMineDestroyAction != null)
            onMineDestroyAction();
    }

    public void BeginExploration()
    {
        gameObject.layer = LayerMask.NameToLayer("ExploredMine");
        isBeingExplored = true;
        if (onBeginExplorationAction != null)
            onBeginExplorationAction();
    }

    public int TakeGold(int maxToTake)
    {
        return Mathf.Clamp(maxToTake, 0, remainingGold);
    }
}
