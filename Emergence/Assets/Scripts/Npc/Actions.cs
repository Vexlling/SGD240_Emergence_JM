using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public enum ActionType
{
    Wander,
    EatDesired,
    EatClosest,
    Idle
}

public class Actions : MonoBehaviour
{

    // set destination for the pathfinder
    // execute selected action
    // Basic action list: wander, eat closest, eat desired, runaway

    [SerializeField] private float movementSpeed = 1.0f;

    GridManager gridManager;
    GridPathfinding pathFinder;
    Spawner spawner;
    GridNode node;

    NpcController npc;
    Trackers trackers;
    Unit unit;

    //GameObject prefab;
    Transform prefab;
    private List<GridNode> positions = new List<GridNode>();
    List<GridNode> path = new List<GridNode>();
    bool movedOnce = false;

    private List<Unit> proximity = new List<Unit>();


    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = GetComponent<GridPathfinding>();
        prefab = GetComponent<Transform>();

        npc = GetComponent<NpcController>();
        unit = GetComponent<Unit>();

        trackers = GetComponent<Trackers>();

        spawner = GetComponentInParent<Spawner>();
        if (spawner != null)
        {
            positions = spawner.Positions;
        }

        if (trackers != null)
        {
            proximity = trackers.proximity;
        }
    }

    void Update()
    {
        //trackers.CalculatePrxoimity();
        //if (proximity.Count > 0)
        //{
            if (spawner.groupSpawned && !movedOnce)
            {
                LocateTarget(unit.location);

                movedOnce = true;

                //Debug.LogWarning("StartCords = " + startCords);
                //Debug.LogWarning("targetCords = " + targetCords);
            }
        //}
    }

    private void ExecutableActions(ActionType action)
    {
        //Vector2Int startCords = new Vector2Int((int)prefab.transform.position.x, (int)prefab.transform.position.y) / gridManager.UnityGridSize;
        // current position

        // Vector2Int targetCords Types
        // A: random index destination within close range
        // B: closest desired gameobject 
        // C: closest gameobject on WillEat List

        switch (action)
        {
            case ActionType.Wander:

                // execute instructions
                //pathFinder.SetNewDestination(startCords, A);

                break;
            case ActionType.EatDesired:

                // execute instructions
                //pathFinder.SetNewDestination(startCords, B);
                // Eat(desired);

                break;
            case ActionType.EatClosest:

                // execute instructions
                //pathFinder.SetNewDestination(startCords, C);
                // Eat(closest);

                break;
            default: // Idle

                // execute instructions
                Debug.Log("Idle");

                break;
        }
    }

    void Eat(GameObject prefab)
    {
        // on collision
            // add nutritional value to hunger status
            // remove collided from map prefab list
            // destroy collided
    }



    //                       //
    // ==== Pathfinding ==== //
    //                       //

    public void LocateTarget(Vector2Int location)
    {
        // Vector2Int targetCords = /*trackers.closestProx.location*/ positions[0].cords; // first spawned spore in scene regardless of proximity
        Vector2Int targetCords = location;

        Vector2Int startCords = new Vector2Int((int)prefab.transform.position.x, (int)prefab.transform.position.y) / gridManager.UnityGridSize;
        // current position

        pathFinder.SetNewDestination(startCords, targetCords);
        RecalculatePath(true);

        //trackers.CalculatePrxoimity();
    }

    void RecalculatePath(bool resetPath)
    {
        //Debug.Log("Recalculate path called");
        Vector2Int coordinates = new Vector2Int();
        if (resetPath)
        {
            coordinates = pathFinder.StartCords;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
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
                travelPercent += Time.deltaTime * movementSpeed;
                prefab.position = Vector2.Lerp(startPosition, endPosition, travelPercent); // shouldn't affect z-axis but it does
                yield return new WaitForEndOfFrame();

                unit.CurrentLocation();
            }
        }   
    }

}
