using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Miner : PathfindingAgent
{
    public Mine mineRef;
    public GameObject baseRef;
    public int maxGoldToTake;
    public int goldToTakePerSecond;

    public Canvas canvas;
    public TextMeshProUGUI text;
    float currentGold;

    override protected void Start()
    {
        base.Start();

        //canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        canvas.transform.LookAt(canvas.transform.position + Vector3.forward);
        int gold = (int)currentGold;
        text.text = gold.ToString();
    }

    public void Mine(float minedGold)
    {
        if (minedGold > 0)
            currentGold += minedGold;
        else
            GetComponent<Animator>().SetTrigger("reachedBase");
    }

    public bool IsFull()
    {
        return currentGold >= maxGoldToTake;
    }

    public int DropGold()
    {
        gameObject.layer = LayerMask.NameToLayer("Miner");
        int gold = (int)currentGold;
        currentGold = 0;
        return gold;
    }
}
