using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fGridNode
{
    /*

    public Vector2Int cords;
    public bool walkable;
    // public bool IsEmpty = true;
    public bool explored;
    public bool path; // final path
    public GridNode connectTo; // Parent

    //
    public GridNode Parent {  get; private set; }
    public int G { get; private set; }
    public int H { get; private set; }
    public int F { get; private set; }

    //public float gCost = float.MaxValue;           // distance from start node to current node
    //public float hCost;                           // estimated distance from current node to target node
    //public float fCost;                          // sum of g & h 
    /*public float FCost()
    {
        return gCost + hCost;
    }*//*

    public GridNode(Vector2Int cords, bool walkable)
    {
        this.cords = cords;
        this.walkable = walkable;
    }   

    public void CalcValues(GridNode parent, GridNode goal, int gCost) // get distance
    {
        this.Parent = parent;
        this.G = parent.G + gCost;
        this.H = (Math.Abs(cords.x - goal.cords.x) + Math.Abs(cords.y - goal.cords.y)) * 10; // might be (goal.cords.y - cords.y)
        this.F = G + H;
    }*/
}

