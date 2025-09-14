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

    public GridNode(Vector2Int cords, bool walkable)
    {
        this.cords = cords;
        this.walkable = walkable;
    }   
}

