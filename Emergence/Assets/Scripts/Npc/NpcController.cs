using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    // Similar to UnitController except for individual use
    // Will replace UnitController later down the line
    
    // Hub for internal parts to communicate and interact with each other

    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private int maxHunger = 1;

    [SerializeField] private GameObject[] willEat;
    [SerializeField] private GameObject desired;

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
    }

    UnitManager unitManager;*/

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
        //unitManager = GetComponentInParent<UnitManager>(); // not sure this works
    }

    
    void Update()
    {
        
    }
}
