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
    [HideInInspector] public int hunger = 100; // spore will have this too, just ignore it for now
    // true hunger value, tweakable by other scripts

    // for distance calc
    public Vector2Int location; // hide later
    [HideInInspector] public int hierarchicalCost;
    public List<Unit> connections; // for proximity 

    
    

    public Unit(PrefabType type, int pips, Vector2Int location, int hCost, List<Unit> connections, int health, int hunger)
    {
        // static
        this.type = type;
        this.nValue = pips;

        // fluid
        this.location = location;
        this.hierarchicalCost = hCost;
        this.connections = connections; // just realised spores don't need a connections list
        this.health = health;
        this.hunger = hunger;
    }

    GridManager gridManager;
    UnitManager unitManager;
    GameObject prefab;

    private void Start()
    {
        // assaigning static values
        nValue = nutritionalValue;
        type = Type;
        //Debug.Log("nValue: "+nValue);

        gridManager = FindObjectOfType<GridManager>();
        unitManager = GetComponentInParent<UnitManager>();
        prefab = GetComponent<GameObject>();


        //CurrentLocation(); // moved to AddConnections function in UnitManager

        unitManager.AddToQueue(this);
    }

    private void Update()
    {
        if (health <= 0) // health should never drop negative, but just in case
        {
            UnitDeath();
        }
    }

    public void CurrentLocation()
    {
        location = gridManager.GetCoordinatesFromPosition(transform.position);
    }

    private void UnitDeath() // has to be here because spore needs access to this
    {
        unitManager.RemoveConnection(this);
        // might need something here for spawner
        Debug.Log("unit " + this.type + " has died");
        Destroy(prefab); // only this class will be able to destroy the prefab, other scripts can only set unit health to 0
    }

}
