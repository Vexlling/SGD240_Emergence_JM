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
    public GridNode connectTo;

    public float gCost = float.MaxValue;           // distance from start node to current node
    public float hCost;                           // estimated distance from current node to target node
    public float fCost;                          // sum of g & h 
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

