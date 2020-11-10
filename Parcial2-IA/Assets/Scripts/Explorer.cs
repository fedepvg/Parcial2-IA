using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : PathfindingAgent
{
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        GetPathToRandomLocation();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }
}
