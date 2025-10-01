using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

[ExecuteAlways]
public class Labeller : MonoBehaviour
{
    // Following StringCode's Unity Grid Based Movement System: Part 1 Setup 
    // Following StringCode's Unity Grid Based Movement System: Part 2 Breadth First Search

    TextMeshPro label;
    public Vector2Int cords = new Vector2Int(); // Specified as public because it was throwing a CS0122 Error in the UnitController Script
    GridManager gridManager;


    [SerializeField] Color defaultColour = Color.white;
    [SerializeField] Color blockedColour = Color.red;
    [SerializeField] Color exploredColour = Color.yellow;
    [SerializeField] Color pathColour = new Color(1f, 0.5f, 0f);

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponentInChildren<TextMeshPro>();
        label.enabled = false; 
        DisplayCords();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            label.enabled = true;
        }
        
        DisplayCords();
        transform.name = cords.ToString();

        ToggleLabels();
        SetLabelColour();
    }

    void SetLabelColour()
    {
        if (gridManager == null) { return; }

        GridNode node = gridManager.GetNode(cords);

        if (node == null) { return; }

        if (!node.walkable)
        {
            label.color = blockedColour;
        }
        else if (node.path)
        {
            label.color = pathColour;
        }
        else if (node.explored)
        {
            label.color = exploredColour;
        }
        else
        {
            label.color = defaultColour;
        }

    }

    private void DisplayCords()
    {
        if (!gridManager) { return; }
        cords.x = Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize);
        cords.y = Mathf.RoundToInt(transform.position.y / gridManager.UnityGridSize); // transform.position.z not needed for 2 dimensions

        label.text = $"{cords.x}, {cords.y}";
    }

    void ToggleLabels()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

}
       

