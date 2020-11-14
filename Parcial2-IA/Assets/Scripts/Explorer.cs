using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : PathfindingAgent
{
    public GameObject mineFlagPrefab;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    public void PutFlag()
    {
        Instantiate(mineFlagPrefab, transform.position, transform.rotation);
    }
}
