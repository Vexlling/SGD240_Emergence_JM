using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{


    // public move controller
    public GridPathfinding pathfinder { get; set; }

    public UtilityAi utilityAi { get; set; }
    public Action[] actionsAvailable;
    // public ActionType actionType { get; set; }



    // script for all creature prefabs only (Spore will not have a NPC Controller)
    // Hub for internal parts to communicate and interact with each other



    [SerializeField] public int maxHunger = 100;
    public int tempClosest = 10;
    public int tempDesired = 10;
    public string chosenAction;
    //[SerializeField] private int nutritionalValue;

    [SerializeField] public PrefabType[] willEat;
    [SerializeField] public PrefabType desired;


    NpcController thisNpc;
    GameObject body;

    Unit thisUnit;

    UnitManager unitManager;

    GridManager gridManager;
    //GridPathfinding pathFinder; 
    //GridAStar pathFinder; 

    // Actions actions
    //UtilityAi utilityAi;


    void Start()
    {
        pathfinder = GetComponent<GridPathfinding>();
        utilityAi = GetComponent<UtilityAi>();



        gridManager = FindObjectOfType<GridManager>();
        
        //pathFinder = GetComponent<GridAStar>();

        
        unitManager = GetComponentInParent<UnitManager>();

        body = GetComponent<GameObject>();

        thisUnit = GetComponent<Unit>();
        //unitManager.AddConnection(thisUnit);
        //unitManager.prefabsInScene.Add(thisNpc);
    }

    
    void Update()
    {
        if (maxHunger <= 0)
        {
            unitManager.RemoveConnection(thisUnit);
            // delete/remove prefab
            Debug.Log("unit " + thisUnit.type + " has died");
        }
        
        if (utilityAi.finishedDeciding)
        {
            utilityAi.finishedDeciding = false;
            utilityAi.bestAction.Execute(this);
        }

        // set current gridnode sat on isEmpty = false;

        //CurrentLocation();
    }

    public void OnFinishedAction()
    {
        utilityAi.DecideBestAction(actionsAvailable);
    }

    #region Coroutine

    public void DoEatDesired(int time)
    {
        StartCoroutine(EatDesiredCoroutine(time));
    }

    public void DoEatClosest(int time)
    {
        StartCoroutine(EatClosestCoroutine(time));
    }

    public void DoIdle(int time)
    {
        StartCoroutine(IdleCoroutine(time));
    }

    IEnumerator EatDesiredCoroutine(int time)
    {
        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }

        Debug.Log("Just Ate 1 desired");
        // logic to update hunger

        OnFinishedAction();
    }

    IEnumerator EatClosestCoroutine(int time)
    {
        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }

        Debug.Log("Just Ate 1 closest");
        // logic to update hunger

        OnFinishedAction();
    }

    IEnumerator IdleCoroutine(int time)
    {
        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }

        Debug.Log("I'm idle");
        // logic to update hunger

        // decide our new best action after you finished this one

        OnFinishedAction();
    }


    #endregion
}
