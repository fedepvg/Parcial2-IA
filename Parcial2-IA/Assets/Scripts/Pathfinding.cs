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
}

public class Pathfinding : MonoBehaviour
{
	public Transform seeker, target;
	MGrid grid;
	public Vector2 currentStart;
	public Vector2 currentTarget;

	void Awake()
	{
		grid = GetComponent<MGrid>();
	}

	void Update()
	{ 
		//if(Input.GetKeyDown(KeyCode.Space))
			FindPath(seeker.position, target.position);
	}

	Path FindPath(Vector3 startPos, Vector3 targetPos)
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
}
