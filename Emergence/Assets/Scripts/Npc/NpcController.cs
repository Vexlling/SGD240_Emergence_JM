using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PrefabType
{
    A,
    B,
    Spore
}

public class NpcController : MonoBehaviour
{

    // Hub for internal parts to communicate and interact with each other

    // naming the PrefabType prefabType, feels like it could run into some issues, hence creatureType
    [SerializeField] private PrefabType creatureType;

    [SerializeField] public float movementSpeed = 1.0f;

    [SerializeField] private int maxHunger;
    [SerializeField] private int nutritionalValue;

    [SerializeField] public PrefabType[] willEat;
    [SerializeField] public PrefabType desired;

    public Vector2Int location;

    [HideInInspector] public int hierarchicalCost;
    NpcController thisNpc;
    GameObject body;

    // should I make this it's own script?
    public NpcController(Vector2Int location, int hCost, GameObject body)
    {
        this.location = location;
        this.hierarchicalCost = hCost;
        this.body = body;
    }

    /*
    private GameObject spore;
    private GameObject creatureA;
    private GameObject creatureB;

    //ref to:
    UnitManager prefab;

    private void SetPrefabCorrelation() // call on awake or start?
    {
        spore = prefab.spore;
        creatureA = prefab.creatureA;
        creatureB = prefab.creatureB;
    }*/

    UnitManager unitManager;

    GridManager gridManager;
    GridPathfinding pathFinder; 
    //GridAStar pathFinder; 

    // Actions actions
    UtilityAi utilityAi;


    /*public Unit(Vector2Int location, bool walkable)
    {
        //this.GameObject = cords;
        this.location = location;
        return;
    */

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();

        pathFinder = GetComponent<GridPathfinding>();
        //pathFinder = GetComponent<GridAStar>();

        utilityAi = GetComponent<UtilityAi>();
        unitManager = GetComponentInParent<UnitManager>();

        body = GetComponent<GameObject>();
        
        
        unitManager.prefabsInScene.Add(thisNpc);
    }

    
    void Update()
    {
        
        // set current gridnode sat on isEmpty = false;

        //CurrentLocation();
    }

    public void CurrentLocation()
    {
        location = gridManager.GetCoordinatesFromPosition(transform.position);
    }
}
