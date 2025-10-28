using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // script for all prefabs

    
    [SerializeField] private PrefabType type;

    public Vector2Int location; // hide later when functionality is proven
    [HideInInspector] public int hierarchicalCost;

    [SerializeField] private int nutritionalValue;


    public Unit(Vector2Int location, PrefabType type, int hCost)
    {
        this.location = location;
        this.type = type;
        this.hierarchicalCost = hCost;
    }


    Unit unit;
    GridManager gridManager;
    UnitManager unitManager;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        unitManager = GetComponentInParent<UnitManager>();

        // all units need to be added to the list including spores
        unitManager.prefabsInScene.Add(unit);
    }

    // can't have this in NpcController if spore prefabs need access to it too.
    public void CurrentLocation()
    {
        location = gridManager.GetCoordinatesFromPosition(transform.position);
    }
}
