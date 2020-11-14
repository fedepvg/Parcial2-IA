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

    public float remainingGold;

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
        if (onBeginExplorationAction != null)
            onBeginExplorationAction();
    }

    public float TakeGold(float maxToTake)
    {
        float goldToTake = Mathf.Clamp(maxToTake, 0, remainingGold);
        remainingGold -= goldToTake;
        return goldToTake;
    }

    public bool IsEmpty()
    {
        return remainingGold <= 0;
    }
}
