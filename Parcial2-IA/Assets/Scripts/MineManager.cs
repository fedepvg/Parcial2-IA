using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    public GameObject minePrefab;
    public int maxUnexploredMines;
    public float mineSpawnCooldown;

    List<GameObject> unexploredMines;
    List<GameObject> exploredMines;
    float mineSpawnTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        unexploredMines = new List<GameObject>();
        exploredMines = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (unexploredMines.Count >= maxUnexploredMines)
            return;

        mineSpawnTimer += Time.deltaTime;
        if(mineSpawnTimer >= mineSpawnCooldown)
        {
            Node mineNode = Pathfinding.Instance.TryGetRandomFreeNode();
            if (mineNode != null)
            {
                GameObject mine = Instantiate(minePrefab, mineNode.worldPosition, Quaternion.identity);
                unexploredMines.Add(Instantiate(minePrefab, mineNode.worldPosition, Quaternion.identity));
                Mine mineComponent = mine.GetComponent<Mine>();
                mineComponent.onBeginExplorationAction = () =>
                {
                    unexploredMines.Remove(mine);
                    exploredMines.Add(mine);
                };
                mineComponent.onMineDestroyAction += () =>
                {
                    exploredMines.Remove(mine);
                };
                mineSpawnTimer = 0;
            }
        }
    }
}
