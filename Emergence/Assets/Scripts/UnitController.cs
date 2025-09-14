using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    // Following StringCode's Unity Grid Based Movement System: Part 1 Setup 

    [SerializeField] float movementSpeed = 1.0f;
    
    Transform selectedUnit;
    bool unitSelected = false;

    GridManager gridManager;

    
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool hasHit = Physics.Raycast(ray, out hit);

            if(hasHit)
            {
                if(hit.transform.tag == "Tile")
                {
                    if(unitSelected)
                    {
                        Vector2Int targetCords = hit.transform.GetComponent<Labeller>().cords;
                        Vector2Int startCords = new Vector2Int((int) selectedUnit.position.x, (int) selectedUnit.position.y) / gridManager.UnityGridSize;

                        selectedUnit.transform.position = new Vector2(targetCords.x/*, selectedUnit.position.y*/, targetCords.y);
                        // Changed Vector3 to Vector2 because I don't need the 3rd dimension
                        // Also it caused the unit to move along the z-axis instead of the y
                    }
                }

                if (hit.transform.tag == "Unit")
                {
                    selectedUnit = hit.transform;
                    unitSelected = true;
                }
            }
        }
    }
}
