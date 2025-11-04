using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class fGridAStar : MonoBehaviour
{
    /*
    [SerializeField] Vector2Int startCords;
    public Vector2Int StartCords { get { return startCords; } }

    [SerializeField] Vector2Int targetCords;
    public Vector2Int TargetCords { get { return targetCords; } }

    GridNode startNode;
    GridNode targetNode;
    GridNode currentNode;

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

    public List<GridNode> GetNewPath() // change to void if needed
    {
        return GetNewPath(startCords);
    }

    public List<GridNode> GetNewPath(Vector2Int coordinates) // change to void if needed // (Vector2Int startcords, Vector2Int targetcords)?
    {
        gridManager.ResetNodes();

        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.walkable = true;
        targetNode.walkable = true;

        HashSet<GridNode> openList = new HashSet<GridNode>();
        HashSet<GridNode> closedList = new HashSet<GridNode>();

        //Stack<GridNode> finalPath = new Stack<GridNode>(); // BuildPath();

        currentNode = startNode; //grid[startCords];

        openList.Add(currentNode);

        bool isRunning = true;

        while (openList.Count > 0 && isRunning == true) // && isRunning == true
        {

            foreach (Vector2Int direction in searchOrder)
            {
                Vector2Int neighbourCords = currentNode.cords + direction;
                // neighbourCords == neighbourPos

                if (grid.ContainsKey(neighbourCords) && grid[neighbourCords].walkable) // if (walkable == true) check has to be here
                {
                    int gCost = 0;

                    if (Math.Abs(neighbourCords.x - neighbourCords.y) == 1)
                    {
                        gCost = 10; // vertical/horizontal cost
                    }
                    else
                    {
                        if (!ConnectedDiagonally(currentNode, grid[neighbourCords])) // corner cutting
                        {
                            continue; 
                        } // corner cutting
                        gCost = 14; // diagonal cost

                    }

                    GridNode neighbour = grid[neighbourCords];

                    // if (neighbour.walkable)
                    //neighbours.Add(grid[neighbourCords]);
                    if (openList.Contains(neighbour))
                    {
                        if (currentNode.G + gCost > neighbour.G)
                        {
                            neighbour.GetScores(currentNode, targetNode, gCost);
                        }
                    }
                    else if (!closedList.Contains(neighbour)) // wlkable check
                    {
                        // new neighbour
                        openList.Add(neighbour);
                        neighbour.GetScores(currentNode, targetNode, gCost);
                    }
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);
            currentNode.explored = true;

            if (openList.Count > 0)
            {
                openList.OrderBy(n => n.F).First(); // sort by f value then take first in ordered list
            }

            if (currentNode.cords == targetCords)
            {
                isRunning = false;
                //currentNode.walkable = false;
                //break;
            }
        }
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

        path.Reverse();
        return path;
    }

    private bool ConnectedDiagonally(GridNode currentNode, GridNode neighbour) // corner cutting
    {
        Vector2Int direction = neighbour.cords - currentNode.cords;

        Vector2Int first = new Vector2Int(currentNode.cords.x + direction.x, currentNode.cords.y);
        Vector2Int second = new Vector2Int(currentNode.cords.x, currentNode.cords.y + direction.y);

        if (grid.ContainsKey(first) && !grid[first].walkable)
        {
            return false;
        }

        if (grid.ContainsKey(second) && !grid[second].walkable)
        {
            return false;
        }

        return true;
    } // corner cutting

    public void SetNewDestination(Vector2Int startCoordinates, Vector2Int targetCoordinates)
    {
        startCords = startCoordinates;
        targetCords = targetCoordinates;
        startNode = grid[this.startCords];
        targetNode = grid[this.targetCords];
        GetNewPath();
    }*/
}
