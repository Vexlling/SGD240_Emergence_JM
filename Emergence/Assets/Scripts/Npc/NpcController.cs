using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using static UnityEditor.FilePathAttribute;
using Unity.VisualScripting;

public class NpcController : MonoBehaviour
{
    // script for moving prefabs only
    //// Hub for internal parts to communicate and interact with each other

 
    // for actions
    public Action[] actionsAvailable;
    public int maxHunger { get; private set; } // read only for consideration scores


    // not sure where to assign, used for changing decisions on the fly.
    // gives weight to previously chosen action if scoring on the fly, should avoid unit getting stuck between two targets

    //public bool alreadyExecutingIdle = false;
    //public bool alreadyExecutingDesired = false; 
    //public bool alreadyExecutingClosest = false;

    public Unit closestProx { get; private set; } // private set so they can be taken into consideation, but not editable 
    public Unit desiredProx { get; private set; }
    Unit bestUnit;
    Unit current;


    // UI - displays actions of a specific prefab
    public string chosenAction;



    // for collision
    private bool lookingForCollision = false; // to allow the possibility to collide with unintentional units 
    private bool hasCollided = false;

    Unit collidedUnit; // to allow the possibility for A to nibble B be accident



    // for movement
    [SerializeField] private float movementSpeed = 2.0f; // inspector set constant
    private float speed; // fluid
    List<GridNode> path = new List<GridNode>(); 




    // Inspector tweakable, read only for other scripts
    [SerializeField] private PrefabType[] WillEat; // list of everything they willing to eat
    public PrefabType[] willEat { get; private set; }

    [SerializeField] private PrefabType Desired;
    public PrefabType desired { get; private set; }



    // temp
    // for collision detection 
    [SerializeField] private GameObject Spore;
    [SerializeField] private GameObject A;
    [SerializeField] private GameObject B;
    // manually link gameobjects because you can't get unit class from gameobjects


    // refs
    UtilityAi utilityAi;
    GridPathfinding pathfinder;
        //GridAStar pathfinder; 
    
    Unit thisUnit;
    UnitManager unitManager;
    GridManager gridManager;

    Transform prefab;
    Spawner spawner;
    Rigidbody2D rb;



    void Start()
    {
        utilityAi = GetComponent<UtilityAi>();
        pathfinder = GetComponent<GridPathfinding>();
        //pathfinder = GetComponent<GridAStar>();

        thisUnit = GetComponent<Unit>();
        unitManager = GetComponentInParent<UnitManager>();
        gridManager = FindObjectOfType<GridManager>();

        prefab = GetComponent<Transform>();
        spawner = GetComponentInParent<Spawner>();
        rb = GetComponent<Rigidbody2D>();


        // assaigning static values
        willEat = WillEat;
        desired = Desired;

        speed = movementSpeed;


        StartCoroutine(UpdateHunger(1)); // decreasing the hunger value here so spore's hunger won't be affected.
    }


    void Update()
    {
        
        if (utilityAi.finishedDeciding)
        {
            utilityAi.finishedDeciding = false;
            utilityAi.bestAction.Execute(this);
        }

        // set current gridnode sat on isEmpty = false;
    }

    IEnumerator UpdateHunger(int seconds)
    {
        // hunger decreases over time, but you have more control then just using DeltaTime
        thisUnit.hunger -= 1;
        maxHunger = thisUnit.hunger;

        yield return new WaitForSeconds(seconds);
    }



    //------------------//
    //      Actions     // 
    //------------------//

    public void OnFinishedAction()
    {
        lookingForCollision = false;
        hasCollided = false;
        utilityAi.DecideBestAction(actionsAvailable);
    }

    void OnCollisionEnter2D(Collision2D collision) // not registering no matter what I do
    {
        // this doesn't even come up so I know it's not the logic inside
        // in theory the below code should work
        Debug.Log("collision called"); 

        if (collision.gameObject == (A || B || Spore) && this.lookingForCollision)
        {
            Debug.Log("unit collision detected");

            foreach (Unit entry in thisUnit.connections) // for int loop with thisUnit.connections wasn't working with unit.body
            {
                if (entry.body == collision.gameObject)
                {
                    collidedUnit = entry;
                    return;
                }

                continue;
            }

            hasCollided = true;
        }
    }


  
    #region Coroutine


    //----------------------//
    //      Eat Actions     //
    //----------------------//
    public void DoEatDesired(int time)
    {
        CalculateProximity();

        // not sure if it's better to set the destination from here
        //LocateTarget(desiredProx.location);

        StartCoroutine(EatChosen(time, desiredProx));

        // again not sure if it's better to make a new decision from here
        //OnFinishedAction();
    }

    public void DoEatClosest(int time) // more or less a copy of DoEatDesired()
    {
        CalculateProximity();
        StartCoroutine(EatChosen(time, closestProx));

        //OnFinishedAction();
    }

    IEnumerator EatChosen(int time, Unit chosen)
    {
        lookingForCollision = true;

        //Debug.Log("chosen type: " + chosen + ", hCost: " + chosen.hierarchicalCost);

        // set target cords to pathfinder
        LocateTarget(chosen.location);
        
        // if at target? if not try again
        if (thisUnit.location != chosen.location) // cheap way, might be better within a while loop
        {
            LocateTarget(chosen.location);
        }


        // in theory once collision is detected then eat - digest - make new decision should function correctly
        if (hasCollided) 
        {
            unitManager.Eat(thisUnit, collidedUnit);

            // time it takes to 'digest' eaten food
            int counter = time;
            while (counter > 0)
            {
                yield return new WaitForSeconds(1);
                counter--;
            }

            OnFinishedAction();
        }
    }


    //---------------------//
    //      Idle Action    //
    //---------------------//
    public void DoIdle(int time)
    {
        // slowly move to nearby tile


        // get list of immediate tiles and choose one at random
        Vector2Int[] immediateDirection = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        int randomSum = UnityEngine.Random.Range(0, immediateDirection.Length);
        Vector2Int randomDirection = immediateDirection[randomSum];

        speed = movementSpeed / 2; // temp half speed for idle movement

        
        // set target cords
        GridNode node = gridManager.GetNode(thisUnit.location + randomDirection); // variable local to function only, used for checks

        if (node != null && node.walkable) // catch out of grid bounds or non walkable destination
        {
            LocateTarget(thisUnit.location + randomDirection);
        }
        else { Debug.Log("Target access is blocked"); } 
    

        // wait for seconds
        StartCoroutine(IdleCoroutine(time));
    }

    IEnumerator IdleCoroutine(int time)
    {
        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }

        speed = movementSpeed; // reset movement speed to original

        OnFinishedAction();
    }


    #endregion



    //---------------------//
    //      Pathfinder     //
    //---------------------//

    public void LocateTarget(Vector2Int location)
    {
        Vector2Int targetCords = location;

        Vector2Int startCords = new Vector2Int((int)prefab.transform.position.x, (int)prefab.transform.position.y) / gridManager.UnityGridSize;
        // current position

        pathfinder.SetNewDestination(startCords, targetCords);
        RecalculatePath(true);
    }

    void RecalculatePath(bool resetPath)
    {
        //Debug.Log("Recalculate path called");
        Vector2Int coordinates = new Vector2Int();
        if (resetPath)
        {
            coordinates = pathfinder.StartCords;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        //StopAllCoroutines();
        StopCoroutine(FollowPath());
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }


    IEnumerator FollowPath()
    {
        //Debug.Log("follow path called");
        for (int i = 1; i < path.Count; i++)
        {
            //Debug.Log("trying to move unit");

            Vector2 startPosition = prefab.position;
            Vector2 endPosition = gridManager.GetPositionFromCoordinates(path[i].cords);
            float travelPercent = 0f;

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                prefab.position = Vector2.Lerp(startPosition, endPosition, travelPercent); // shouldn't affect z-axis but it does

                thisUnit.CurrentLocation(); // updates as unit moves
                
                yield return new WaitForEndOfFrame();
            }
        }
    }


    //--------------------//
    //      Proximity     //
    //--------------------//

    public void CalculateProximity()
    {
  
        // if the group hasn't spawned then generallly this function doesn't have anything to calculate with
        if (!spawner.groupSpawned) { Debug.Log("GroupSpawn needed for connections"); return; }
        
        //Debug.Log("Co count: " + thisUnit.connections.Count);
        if (thisUnit.connections.Count == 0) { Debug.Log("connections empty");  return; } // abort early if empty


        // calculate hCost
        foreach (Unit entry in thisUnit.connections)
        {
            entry.hierarchicalCost = (Math.Abs(thisUnit.location.x - entry.location.x) + Math.Abs(thisUnit.location.y - entry.location.y)) * 10;
        }


        // ORDER LIST

        thisUnit.connections.OrderBy(n => n.hierarchicalCost).ToList(); // ascends
        // will they reassign the hcost for all unit connections list or just for this one?
        


        // DESIRED PROXIMITY

        desiredProx = thisUnit.connections.Find(n => n.type == desired); 
        //Debug.Log("desired type: " + desiredProx + ", hCost: " + desiredProx.hierarchicalCost);



        // CLOSEST PROXIMITY

        bestUnit = null; 

        // seems inefficient but couldn't get other methods to work
        foreach (PrefabType entry in WillEat) 
        {
            current = thisUnit.connections.Find(n => n.type == entry);
            if (bestUnit == null || current.hierarchicalCost < bestUnit.hierarchicalCost) // smaller the better
            { 
                bestUnit = current; 
            }
        }

        closestProx = bestUnit;
        //Debug.Log("closest type: " + closestProx + ", hCost: " + closestProx.hierarchicalCost);

        return;
    }
}
