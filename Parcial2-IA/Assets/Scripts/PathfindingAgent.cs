using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingAgent : MonoBehaviour
{
    Pathfinding pathfinding;
    public Path currentPath;
    public bool pathExists = false;
    public VisionCone visionCone;
    
    public float speed;
    public Vector3 targetPos;

    private void Awake()
    {
        currentPath = new Path();
        visionCone = GetComponent<VisionCone>();
    }

    // Start is called before the first frame update
    virtual protected void Start()
    {
        pathfinding = Pathfinding.Instance;
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dirVector, Vector3.up), 1);

            if (pathfinding.ReachedNode(currentPath.nodeList[0], transform.position))
            {
                currentPath.nodeList.RemoveAt(0);
                if (currentPath.nodeList.Count <= 0)
                {
                    pathExists = false;

                    if (currentPath.onEndPathDelegate != null)
                        currentPath.onEndPathDelegate();
                }
            }
        }
        else
            pathExists = false;
    }

    public void FindPath(Vector3 targetLocation, Path.OnEndPath endPathDelegate = null)
    {
        currentPath = pathfinding.FindPath(transform.position, targetLocation);
        pathExists = true;
        if(endPathDelegate != null)
            currentPath.onEndPathDelegate = endPathDelegate;
    }

    public void GetPathToRandomLocation()
    {
        pathExists = pathfinding.TryGetPathToRandomPoint(transform.position, ref currentPath);
        if(currentPath.pathExists)
            targetPos = currentPath.target.worldPosition;
    }
}
