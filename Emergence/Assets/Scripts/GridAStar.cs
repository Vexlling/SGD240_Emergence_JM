using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAStar : MonoBehaviour
{

    [SerializeField] Vector2Int startCords;
    public Vector2Int StartCords { get { return startCords; } }

    [SerializeField] Vector2Int targetCords;
    public Vector2Int TargetCords { get { return targetCords; } }

    GridNode startNode;
    GridNode targetNode;
    GridNode currentNode;

    
    private List<GridNode> openList = new List<GridNode>();
    private List<GridNode> closedList = new List<GridNode>();

    GridManager gridManager;
    Dictionary<Vector2Int, GridNode> grid = new Dictionary<Vector2Int, GridNode>();

    // clockwise search order for pathfinder algarithim 
    Vector2Int[] searchOrder = { Vector2Int.up, new Vector2Int(1, 1), Vector2Int.right, new Vector2Int(1, -1), Vector2Int.down, new Vector2Int(-1, -1), Vector2Int.left, new Vector2Int(-1, 1) };
  
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
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

        //openList.Clear();
        //closedList.Clear();

        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {   

        startNode.walkable = true;
        targetNode.walkable = true;

        openList.Clear();
        closedList.Clear();

        //openList.Add(startNode);

        bool isRunning = true;

        openList.Add(grid[coordinates]);
        closedList.Add(grid[coordinates]);

        openList.Add(startNode);

        startNode.gCost = 0;
        startNode.hCost = GetDistance(startCords, targetCords);
        startNode.FCost();

        while (openList.Count > 0 && isRunning == true)
        {
            int lowestF = default;

            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].FCost() < openList[lowestF].FCost())
                {
                    lowestF = i;
                }
            }

            currentNode = openList[lowestF];

            if (currentNode.cords == targetCords)
            {
                isRunning = false;
                currentNode.walkable = false;
                Debug.Log("Current == Target");
            }

            openList.Remove(currentNode);
            currentNode.explored = true; 
            closedList.Add(currentNode);
            ExploreNeighbours();
            Debug.Log("explore neighbours was called or null");
        }

        Debug.Log("BFS while loop returned");
        // nodes not possible
        return;
    }

    void ExploreNeighbours()
    {
        //Debug.Log("explore neighbours was called");
        List<GridNode> neighbours = new List<GridNode>();

        foreach (Vector2Int direction in searchOrder)
        {
            Vector2Int neighbourCords = currentNode.cords + direction;

            if (grid.ContainsKey(neighbourCords))
            {
                neighbours.Add(grid[neighbourCords]);
                Debug.Log("neighbourcords added to neighbours");
            }
        }

        foreach (GridNode neighbourNode in neighbours)
        {
            if (closedList.Contains(neighbourNode)/* || !neighbourNode.walkable*/) continue;

            float tentativeGCost = currentNode.gCost + GetDistance(currentNode.cords, neighbourNode.cords);

            if (tentativeGCost < neighbourNode.gCost)
            {
                neighbourNode.connectTo = currentNode;
                neighbourNode.gCost = tentativeGCost;
                neighbourNode.hCost = GetDistance(neighbourNode.cords, targetNode.cords);
                neighbourNode.FCost();

                if (!openList.Contains(neighbourNode) && neighbourNode.walkable)
                {
                    openList.Add(neighbourNode);
                    Debug.Log("New neighbourNode added to openList");
                }
            }
        }
    }

    public int GetDistance(Vector2Int a, Vector2Int b)
    {
        Vector2Int dist = new Vector2Int(Mathf.Abs((int)a.x - (int)b.x), Mathf.Abs((int)a.y - (int)b.y));

        int lowest = Mathf.Min(dist.x, dist.y);
        int highest = Mathf.Max(dist.x, dist.y);

        int horizontalMovesRequired = highest - lowest;

        //Debug.Log("Distance calculated");

        return lowest * 14 + horizontalMovesRequired * 10;

    }

    List<GridNode> BuildPath()
    {
        List<GridNode> path = new List<GridNode>();
        GridNode currentNode = targetNode;

        path.Add(currentNode);
        currentNode.path = true;

        while (currentNode.connectTo != null)
        {
            currentNode = currentNode.connectTo;
            path.Add(currentNode);
            currentNode.path = true;
        }

        Debug.Log("BuildPath Called");

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
