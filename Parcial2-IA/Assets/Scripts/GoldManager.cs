using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    private static GoldManager instance;

    public GameObject explorerPrefab;
    public GameObject minerPrefab;
    public GameObject baseRef;

    public int explorerCost;
    public int minerCost;
    public int startGold;

    public Text goldText;
    public Text explorerCostText;
    public Text minerCostText;
    public Button explorerButton;
    public Button minerButton;

    int prevGold;
    int currentGold;

    public static GoldManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance == null)
            instance = this as GoldManager;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        explorerCostText.text = explorerCost.ToString();
        minerCostText.text = minerCost.ToString();

        currentGold = startGold;
    }

    private void Update()
    {
        if (prevGold != currentGold)
        {
            goldText.text = currentGold.ToString();
            prevGold = currentGold;
        }

        if (currentGold >= explorerCost)
            explorerButton.interactable = true;
        else
            explorerButton.interactable = false;

        if (currentGold >= minerCost)
            minerButton.interactable = true;
        else
            minerButton.interactable = false;
    }

    public void AddGold(int gold)
    {
        currentGold += gold;
    }

    public void CreateExplorer()
    {
        if (currentGold >= explorerCost)
        {
            Instantiate(explorerPrefab, baseRef.transform.position + Vector3.up, Quaternion.identity);
            currentGold -= explorerCost;
        }
    }

    public void CreateMiner()
    {
        if (currentGold >= minerCost)
        {
            GameObject miner = Instantiate(minerPrefab, baseRef.transform.position + Vector3.up, Quaternion.identity);
            miner.GetComponent<Miner>().baseRef = baseRef;
            currentGold -= minerCost;
        }
    }
}
