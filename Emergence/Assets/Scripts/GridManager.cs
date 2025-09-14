using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Following StringCode's Unity Grid Based Movement System: Part 1 Setup 

    [SerializeField] Vector2Int gridSize; 
    [SerializeField] int unityGridSize;
    public int UnityGridSize { get { return unityGridSize; } }

    private Dictionary<Vector2Int, GridNode> grid = new Dictionary<Vector2Int, GridNode>();
    private Dictionary<Vector2Int, GridNode> Grid {  get { return grid; } }

    private void Awake()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int cords = new Vector2Int(x, y); // cords == pos
                grid.Add(cords, new GridNode(cords));


            }
        }
    }
}
