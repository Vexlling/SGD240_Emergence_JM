using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPathfinding : MonoBehaviour
{
    // Following StringCode's Unity Grid Based Movement System: Part 2 Breadth First Search

    // Will need to made for individuality

    [SerializeField] Vector2Int startCords;
    public Vector2Int StartCords { get { return startCords; } }

    [SerializeField] Vector2Int targetCords;
    public Vector2Int TargetCords { get { return targetCords; } }

    GridNode startNode;
    GridNode targetNode;
    GridNode currentNode;

    Queue<GridNode> frontier = new Queue<GridNode>(); // change frontier to openSet for calrity?
    Dictionary<Vector2Int, GridNode> reached = new Dictionary<Vector2Int, GridNode>(); // same but closedSet

    GridManager gridManager;
    Dictionary<Vector2Int, GridNode> grid = new Dictionary<Vector2Int, GridNode>();

    // search order for breadth first search
    Vector2Int[] searchOrder = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down, new Vector2Int(1, 1), new Vector2Int(-1, 1), new Vector2Int(1, -1), new Vector2Int(-1, -1) }; 
    /*
    Diagonal search:  
        Vector2Int(1, 1) = TopRight
        Vector2Int(-1, 1) = TopLeft
        Vector2Int(1, -1) = BottomRight
        Vector2Int(-1, -1) = BottomLeft
    */

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null )
        {
            grid = gridManager.Grid;
        }
    }

    public List<GridNode> GetNewPath()
    {
        return GetNewPath(startCords);
    }

    public List<GridNode> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();

        BreadthFirstSearch(coordinates);
        return BuildPath(); 
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.walkable = true;
        targetNode.walkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while (frontier.Count > 0 && isRunning == true)
        {
            currentNode = frontier.Dequeue();
            currentNode.explored = true;
            ExploreNeighbours();
            if (currentNode.cords == targetCords)
            {
                isRunning = false;
                currentNode.walkable = false;
            }
        }
    }

    void ExploreNeighbours()
    {
        List<GridNode> neighbours = new List<GridNode>();

        foreach (Vector2Int direction in searchOrder)
        {
            Vector2Int neighbourCords = currentNode.cords + direction;

            if (grid.ContainsKey(neighbourCords))
            {
                neighbours.Add(grid[neighbourCords]);
            }
        }

        foreach (GridNode neighbour in neighbours)
        {
            if (!reached.ContainsKey(neighbour.cords) && neighbour.walkable)
            {
                neighbour.connectTo = currentNode;
                reached.Add(neighbour.cords, neighbour);
                frontier.Enqueue(neighbour);
            }
        }
    }

    List<GridNode> BuildPath()
    {
        List<GridNode > path = new List<GridNode>();
        GridNode currentNode = targetNode;

        path.Add(currentNode);
        currentNode.path = true;

        while (currentNode.connectTo != null)
        {
            currentNode = currentNode.connectTo;
            path.Add(currentNode);
            currentNode.path = true;
        }

        path.Reverse();
        return path;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

    public void SetNewDestination(Vector2Int startCoordinates, Vector2Int targetCoordinates)
    {
        startCords = startCoordinates;
        targetCords = targetCoordinates;
        startNode = grid[this.startCords];
        targetNode = grid[this.targetCords];
        GetNewPath();
    }
}
