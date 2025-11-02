using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // script for all prefabs


    // Variables tweakable by the Inspector, but read only for other scripts
    [SerializeField] private PrefabType Type;
    public PrefabType type { get; private set; }


    [SerializeField] private int nutritionalValue; 
    public int nValue { get; private set; }



    // Fully public 

    public int health; // spore = 1, other = 2, so eating bigger take a little longer, which could give them enough hunger to nibble then run away.
    //[HideInInspector] public int maxHunger = 100; // spore will have hunger, but ignore that for now

    // for distance calculations
    public Vector2Int location; // hide later 
    [HideInInspector] public int hierarchicalCost;
    public List<Unit> connections; // for proximity // hide later
 
    

    public Unit(PrefabType type, int pips, Vector2Int location, int hCost, List<Unit> connections, int health/*, int hunger*/)
    {
        // static
        this.type = type;
        this.nValue = pips;

        // fluid
        this.location = location;
        this.hierarchicalCost = hCost;
        this.connections = connections;
        this.health = health;
       // this.maxHunger = hunger;
    }

    GridManager gridManager;
    UnitManager unitManager;

    private void Start()
    {
        // assaigning static values
        nValue = nutritionalValue;
        type = Type;
        //Debug.Log("nValue: "+nValue);

        gridManager = FindObjectOfType<GridManager>();
        unitManager = GetComponentInParent<UnitManager>();


        //CurrentLocation(); // moved to AddConnections function in UnitManager

        unitManager.AddToQueue(this);
    }


    public void CurrentLocation()
    {
        location = gridManager.GetCoordinatesFromPosition(transform.position);
    }


    // put logic here for when unit's health == 0;
    // something like
    public void UnitDeath()
    {
        if (health <= 0) // health should never drop negative, but just in case
        {
            unitManager.RemoveConnection(this);
            Debug.Log("unit " + this.type + " has died");
            // destroy prefab
        }
    }

}
