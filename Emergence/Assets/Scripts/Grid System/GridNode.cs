using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode
{
    // Based on "Unity Grid Based Movement System: Part 1 Setup" by StringCode 
    // Based on "Unity Grid Based Movement System: Part 2 Breadth First Search" by StringCode

    public Vector2Int cords;
    public bool walkable;
    public bool explored;
    public bool path;
    public bool IsEmpty = true; // for moving targets
    public GridNode connectTo;

    public int gCost;           // distance from start node to current node
    public int hCost;                           // estimated distance from current node to target node
    public int fCost;                          // sum of g & h 
    public float FCost()
    {
        return gCost + hCost;
    }

    public GridNode(Vector2Int cords, bool walkable)
    {
        this.cords = cords;
        this.walkable = walkable;
    }   
}

