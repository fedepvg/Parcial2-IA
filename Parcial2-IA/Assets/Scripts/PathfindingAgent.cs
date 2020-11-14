using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingAgent : MonoBehaviour
{
    Pathfinding pathfinding;
    Path currentPath;
    public bool pathExists = false;

    public float speed;
    public Vector3 targetPos;

    public delegate void OnEndNodeReached();
    public static OnEndNodeReached endNodeReachedAction;

    private void Awake()
    {
        currentPath = new Path();
    }

    // Start is called before the first frame update
    virtual protected void Start()
    {
        pathfinding = Pathfinding.Instance;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        //if (pathExists)
        //    FollowPath();
    }

    public void FollowPath()
    {
        if (currentPath.pathExists)
        {
            Vector3 startPos = new Vector3(transform.position.x, 0f, transform.position.z);
            Vector3 targetPos = new Vector3(currentPath.nodeList[0].worldPosition.x, 0f, currentPath.nodeList[0].worldPosition.z);

            Vector3 dirVector = targetPos - startPos;
            dirVector.Normalize();
            transform.position += dirVector * speed * Time.deltaTime;

            if (pathfinding.ReachedNode(currentPath.nodeList[0], transform.position))
            {
                currentPath.nodeList.RemoveAt(0);
                if (currentPath.nodeList.Count <= 0)
                {
                    pathExists = false;

                    if (endNodeReachedAction != null)
                        endNodeReachedAction();
                }
            }
        }
        else
            pathExists = false;
    }

    public void GetPathToRandomLocation()
    {
        pathExists = pathfinding.TryGetPathToRandomPoint(transform.position, ref currentPath);
        if(currentPath.pathExists)
            targetPos = currentPath.target.worldPosition;
    }
}
