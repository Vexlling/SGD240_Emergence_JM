using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // script for all prefabs

    
    public PrefabType type;
    [SerializeField] private int nutritionalValue; // might have to public

    public Vector2Int location; // hide later when functionality is proven
    [HideInInspector] public int hierarchicalCost;

    /*[HideInInspector] public int hCost() // privatising
    {
        return hierarchicalCost;
    }*/


    public List<Unit> connections; // for proximity

    public Unit(Vector2Int location, PrefabType type, int hCost, List<Unit> connections, int pips)
    {
        this.location = location;
        this.type = type;
        this.hierarchicalCost = hCost;
        this.connections = connections;
        this.nutritionalValue = pips;
    }


    Unit thisUnit; // will using just 'this' work?
    GridManager gridManager;
    UnitManager unitManager;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        unitManager = GetComponentInParent<UnitManager>();
        thisUnit = GetComponent<Unit>();

        CurrentLocation();

        // all units need to be added to the list including spores
        unitManager.AddToQueue(thisUnit);
    }

    // can't have this in NpcController if spore prefabs need access to it too.
    public void CurrentLocation()
    {
        location = gridManager.GetCoordinatesFromPosition(transform.position);
    }
}
