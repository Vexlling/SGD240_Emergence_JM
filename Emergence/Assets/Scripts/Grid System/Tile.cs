using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Following "Unity Grid Based Movement System: Part 2 Breadth First Search" by StringCode

    [SerializeField] bool blocked;

    [SerializeField] SpriteRenderer tileSprite;
    [SerializeField] Sprite walkableTile;
    [SerializeField] Sprite blockedTile;

    public Vector2Int cords;

    GridManager gridManager;


    void Start()
    {
        SetCords();

        if (blocked)
        {
            gridManager.BlockNode(cords);
        }
    }

    private void SetCords() // fpr displaying cords
    { 
        gridManager = FindObjectOfType<GridManager>();
        int x = (int)transform.position.x;
        int y = (int)transform.position.y; // orginally int z

        cords = new Vector2Int(x / gridManager.UnityGridSize, y / gridManager.UnityGridSize); // orignally int z
    }
    
    
    public void SetTileSprite() // visually shows the blocked status of blocks in scene
    {
        // called from Labeller script
        if (blocked)
        {
            tileSprite.sprite = blockedTile;
        }
        else
        {
            tileSprite.sprite = walkableTile;
        }
    }
}
