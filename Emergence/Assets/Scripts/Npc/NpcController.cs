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

    // script for all creature prefabs only (Spore will not have a NPC Controller)
    // Hub for internal parts to communicate and interact with each other



    [SerializeField] private int maxHunger;
    //[SerializeField] private int nutritionalValue;

    [SerializeField] public PrefabType[] willEat;
    [SerializeField] public PrefabType desired;

    //public Vector2Int location;

    //[HideInInspector] public int hierarchicalCost;
    NpcController thisNpc;
    GameObject body;

    Unit thisUnit;

    UnitManager unitManager;

    GridManager gridManager;
    GridPathfinding pathFinder; 
    //GridAStar pathFinder; 

    // Actions actions
    UtilityAi utilityAi;


    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();

        pathFinder = GetComponent<GridPathfinding>();
        //pathFinder = GetComponent<GridAStar>();

        utilityAi = GetComponent<UtilityAi>();
        unitManager = GetComponentInParent<UnitManager>();

        body = GetComponent<GameObject>();

        thisUnit = GetComponent<Unit>();
        unitManager.AddConnection(thisUnit);
        //unitManager.prefabsInScene.Add(thisNpc);
    }

    
    void Update()
    {
        
        // set current gridnode sat on isEmpty = false;

        //CurrentLocation();
    }

    /*public void CurrentLocation()
    {
        location = gridManager.GetCoordinatesFromPosition(transform.position);
    }*/
}
