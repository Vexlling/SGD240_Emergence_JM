using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Following StringCode's Unity Grid Based Movement System: Part 2 Breadth First Search

    [SerializeField] bool blocked;

    // [SerializeField] floor sprite 
    // [SerializeField] wall/blocked sprite

    public Vector2Int cords;

    GridManager gridManager;

    // Start is called before the first frame update
    void Start()
    {
        SetCords();

        if (blocked)
        {
            gridManager.BlockNode(cords);
        }
    }

    private void SetCords()
    { 
        gridManager = FindObjectOfType<GridManager>();
        int x = (int)transform.position.x;
        int y = (int)transform.position.y; // orginally int z

        cords = new Vector2Int(x / gridManager.UnityGridSize, y / gridManager.UnityGridSize); // orignally int z
    }
    
    // set tile sprite or colour to chosen one based on bool

}
