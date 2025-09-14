using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class Labeller : MonoBehaviour
{
    // Following StringCode's Unity Grid Based Movement System: Part 1 Setup 

    TextMeshPro label;
    public Vector2Int cords = new Vector2Int(); // Specified as public because it was throwing a CS0122 Error in the UnitController Script
    GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponentInChildren<TextMeshPro>();

        DisplayCords();
    }

    private void Update()
    {
        DisplayCords();
        transform.name = cords.ToString();
    }

    private void DisplayCords()
    {
        if (!gridManager) { return; }
        cords.x = Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize);
        cords.y = Mathf.RoundToInt(transform.position.y / gridManager.UnityGridSize); // transform.position.z not needed for 2 dimensions

        label.text = $"{cords.x}, {cords.y}";
    }
}
       

