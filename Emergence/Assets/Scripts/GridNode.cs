using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode
{
    // Following StringCode's Unity Grid Based Movement System: Part 1 Setup 
    // Following StringCode's Unity Grid Based Movement System: Part 2 Breadth First Search

    public Vector2Int cords;
    public bool walkable;
    public bool explored;
    public bool path;
    public GridNode connectTo; // change to parent for clarity?

    // public int/float gCost                       distance from start node to current node
    // public int/float hCost                       estimated distance from current node to target node
    // public int/float fCost = gCost + hCost       sum of g and h

    public GridNode(Vector2Int cords, bool walkable)
    {
        this.cords = cords;
        this.walkable = walkable;
    }   
}

