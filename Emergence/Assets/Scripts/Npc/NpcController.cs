using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    // script for moving prefabs only
    //// Hub for internal parts to communicate and interact with each other

 
    // for actions
    public Action[] actionsAvailable;
    [SerializeField] public int maxHunger = 100;
    public int tempClosest = 10;
    public int tempDesired = 10;

        // displays actions of a specific prefab
        public string chosenAction;


    // for movement
    [SerializeField] private float movementSpeed = 2.0f;
    List<GridNode> path = new List<GridNode>();
    
    bool movedOnce = false;

        // set target cords to check pathfinder is working
        public Vector2Int temp;




    // Inspector tweakable, read only for other scripts
    [SerializeField] private PrefabType[] WillEat;
    public PrefabType[] willEat { get; private set; }

    [SerializeField] private PrefabType Desired;
    public PrefabType desired { get; private set; }



    // refs
    UtilityAi utilityAi;
    GridPathfinding pathfinder;
        //GridAStar pathfinder; 
    
    Unit thisUnit;
    UnitManager unitManager;
    GridManager gridManager;

    Transform prefab;

    Spawner spawner;



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
        


        // assaigning static values
        willEat = WillEat;
        desired = Desired;
    }


    void Update()
    {
        
        if (utilityAi.finishedDeciding)
        {
            utilityAi.finishedDeciding = false;
            utilityAi.bestAction.Execute(this);
        }

        // maxHunger -= 1 // decrease overtime

        // set current gridnode sat on isEmpty = false;

        //CurrentLocation();

        if (spawner.groupSpawned && !movedOnce)
        {
            LocateTarget(temp);

            movedOnce = true;

            //Debug.LogWarning("StartCords = " + startCords);
            //Debug.LogWarning("targetCords = " + targetCords);
        }
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

    public void LocateTarget(Vector2Int location)
    {
        // Vector2Int targetCords = /*trackers.closestProx.location*/ positions[0].cords; // first spawned spore in scene regardless of proximity
        Vector2Int targetCords = location;

        Vector2Int startCords = new Vector2Int((int)prefab.transform.position.x, (int)prefab.transform.position.y) / gridManager.UnityGridSize;
        // current position

        pathfinder.SetNewDestination(startCords, targetCords);
        RecalculatePath(true);

        //trackers.CalculatePrxoimity();
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

        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    // this should be worked into the actions coroutines
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
                travelPercent += Time.deltaTime * movementSpeed;
                prefab.position = Vector2.Lerp(startPosition, endPosition, travelPercent); // shouldn't affect z-axis but it does
                yield return new WaitForEndOfFrame();

                thisUnit.CurrentLocation();
            }
        }
    }

}
