using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Following StringCode's Unity Grid Based Movement System: Part 1 Setup 
    // Following StringCode's Unity Grid Based Movement System: Part 2 Breadth First Search

    [SerializeField] Vector2Int gridSize; 
    [SerializeField] int unityGridSize;
    public int UnityGridSize { get { return unityGridSize; } }

    private Dictionary<Vector2Int, GridNode> grid = new Dictionary<Vector2Int, GridNode>(); // might need to be public too
    public Dictionary<Vector2Int, GridNode> Grid {  get { return grid; } } 

    private void Awake()
    {
        CreateGrid();
    }

    // update void to check tile availability
    // if tile suddenly becomes available set explored to false and add to blocked node list is suddenly blocked
    // might need to defrentiate between perm block and temp block nodes

    public GridNode GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }

        return null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].walkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, GridNode> entry in grid)
        {
            entry.Value.connectTo = null;
            entry.Value.explored = false;
            entry.Value.path = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector2 position) // Changed from Vector3
    {
        Vector2Int coordinates = new Vector2Int();

        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.y / unityGridSize); // changed from position.z

        return coordinates;
    }

    public Vector2 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector2 position = new Vector2(); // changed from Vector3

        position.x = coordinates.x * unityGridSize;
        position.y = coordinates.y * unityGridSize; // changed from position.z

        return position; // CS0161 Error: not all code paths return a value
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int cords = new Vector2Int(x, y); // cords == pos
                grid.Add(cords, new GridNode(cords, true));
            }
        }
    }
    
}
