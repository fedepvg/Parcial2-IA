using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGrid : MonoBehaviour
{
	public Vector2 gridWorldSize;
	public float nodeRadius;
	public Vector2 currentNode;

	Node[,] grid;
	BoxCollider boxVolume;
	float nodeDiameter;
	int gridSizeX;
	int gridSizeY;

	void Awake()
	{
		nodeDiameter = nodeRadius * 2;
		boxVolume = GetComponent<BoxCollider>();
		CreateGrid();
	}

	void CreateGrid()
	{
		Vector3 worldBottomLeft = new Vector3(boxVolume.bounds.min.x, boxVolume.bounds.max.y, boxVolume.bounds.min.z);

		gridSizeX = Mathf.RoundToInt(boxVolume.bounds.size.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(boxVolume.bounds.size.z / nodeDiameter);

		grid = new Node[gridSizeX, gridSizeY];

		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = Random.Range(0f, 1f) < 0.85f ? true : false;
				grid[x, y] = new Node(walkable, worldPoint, x, y);
			}
		}
	}

	public List<Node> GetNeighbours(Node node)
	{
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
				{
					neighbours.Add(grid[checkX, checkY]);
				}
			}
		}

		return neighbours;
	}


	public Node GetNodeFromWorldPosition(Vector3 worldPosition)
	{
		Vector3 worldBottomLeft = new Vector3(boxVolume.bounds.min.x, boxVolume.bounds.max.y, boxVolume.bounds.min.z);
		Vector3 worldTopRight = new Vector3(boxVolume.bounds.max.x, boxVolume.bounds.max.y, boxVolume.bounds.max.z);

		float maxX = worldTopRight.x - worldBottomLeft.x;
		float maxY = worldTopRight.z - worldBottomLeft.z;

		float yPerc = (worldPosition.z - worldBottomLeft.z - nodeRadius) / maxY;
		float xPerc = (worldPosition.x - worldBottomLeft.x - nodeRadius) / maxX;

		int x = Mathf.Clamp(Mathf.RoundToInt((gridSizeX) * xPerc), 0, gridSizeX - 1);
		int y = Mathf.Clamp(Mathf.RoundToInt((gridSizeY) * yPerc), 0, gridSizeY - 1);

		return grid[x, y];
	}
}
