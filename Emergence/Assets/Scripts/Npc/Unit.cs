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
    //[HideInInspector] public Unit connection; // for proximity // connection = hcost
    public Dictionary<Unit, int> connections;

    public Unit(Vector2Int location, PrefabType type, int hCost, Dictionary<Unit, int> connections)
    {
        this.location = location;
        this.type = type;
        this.hierarchicalCost = hCost;
        this.connections = connections;
    }


    Unit thisUnit;
    GridManager gridManager;
    //UnitManager unitManager;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        //unitManager = GetComponentInParent<UnitManager>();
        //unit = GetComponent<Unit>();

        // all units need to be added to the list including spores
        //unitManager.prefabsInScene.Add(unit);
        
        //unitManager.AddConnection(unit);

        CurrentLocation();

        //unitManager.AddConnection(thisUnit);
    }

    // can't have this in NpcController if spore prefabs need access to it too.
    public void CurrentLocation()
    {
        location = gridManager.GetCoordinatesFromPosition(transform.position);
    }
}
