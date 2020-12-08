using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Path
{
	public bool pathExists;
	public Node start;
	public Node target;
	public List<Node> nodeList;
	public delegate void OnEndPath();
	public OnEndPath onEndPathDelegate;
}

public class Pathfinding : MonoBehaviour
{
	MGrid grid;
	
	public float distanceToReachNode;

	public Transform seeker, target;
	public Vector2 currentStart;
	public Vector2 currentTarget;
	public int maxAttemptsToGetRandomPoint;
	

	private static Pathfinding instance;

	public static Pathfinding Instance
	{
		get { return instance; }
	}

	void Awake()
	{
		if (instance == null)
			instance = this as Pathfinding;
		else
			Destroy(gameObject);

		grid = GetComponent<MGrid>();
	}

	public Path FindPath(Vector3 startPos, Vector3 targetPos)
	{
		Path currentPath = new Path();

		Node startNode = grid.GetNodeFromWorldPosition(startPos);
		Node endNode = grid.GetNodeFromWorldPosition(targetPos);

		currentPath.start = startNode;
		currentPath.target = endNode;

		List<Node> openList = new List<Node>();
		List<Node> closedList = new List<Node>();

		openList.Add(startNode);

		Node currentNode = startNode;

		while (openList.Count > 0)
		{
			float lowestCost = Single.MaxValue;

			foreach(Node node in openList)
            {
				if(node.fCost < lowestCost)
                {
					lowestCost = node.fCost;
					currentNode = node;
                }
            }

			openList.Remove(currentNode);
			closedList.Add(currentNode);

			if (currentNode == endNode)
			{
				currentPath.nodeList = new List<Node>(RetracePath(startNode, endNode));
				currentPath.pathExists = true;
				grid.path = currentPath;
				return currentPath;
			}

			foreach(Node node in grid.GetNeighbours(currentNode))
            {
				if (closedList.Contains(node) || !node.walkable)
					continue;
				node.gCost = GetDistance(currentNode, node);
				node.hCost = GetDistance(node, endNode);
				node.parent = currentNode;
				if (!openList.Contains(node))
					openList.Add(node);
            }
		}

		currentPath.pathExists = false;
		return currentPath;
	}

	List<Node> RetracePath(Node startNode, Node endNode)
	{
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Add(startNode);
		path.Reverse();

		return path;
	}

	float GetDistance(Node nodeA, Node nodeB)
	{
		return Vector3.Distance(nodeA.worldPosition, nodeB.worldPosition);
	}

	public Node GetNode(Vector3 position)
    {
		return grid.GetNodeFromWorldPosition(position);
    }

	public bool ReachedNode(Node node, Vector3 position)
    {
		Vector2 startPos = new Vector2(node.worldPosition.x, node.worldPosition.z);
		Vector2 targetPos = new Vector2(position.x, position.z);

		if(Vector2.Distance(startPos, targetPos) <= distanceToReachNode)
			return true;

		return false;
    }

	public bool TryGetPathToRandomPoint(Vector3 currentPosition, ref Path path)
    {
		Node currentNode = grid.GetNodeFromWorldPosition(currentPosition);

		Node target = grid.TryGetRandomPoint(maxAttemptsToGetRandomPoint, currentNode);

		if (target != null)
		{
			path = FindPath(currentNode.worldPosition, target.worldPosition);
			return true;
		}

		return false;
    }

	public Node TryGetRandomFreeNode()
	{
		return grid.TryGetRandomPoint(maxAttemptsToGetRandomPoint, null);
	}
}
