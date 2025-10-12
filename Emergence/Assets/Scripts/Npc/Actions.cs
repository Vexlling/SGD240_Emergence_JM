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

    GridManager gridManager;
    GridPathfinding pathFinder;
    Spawner spawner;
    GridNode node;

    //GameObject prefab;
    Transform prefab;
    private List<GridNode> positions = new List<GridNode>();
    List<GridNode> path = new List<GridNode>();
    bool movedOnce = false;


    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = GetComponent<GridPathfinding>();
        prefab = GetComponent<Transform>();

        spawner = GetComponentInParent<Spawner>();
        if (spawner != null)
        {
            positions = spawner.Positions;
        }
    }

    void Update()
    {
        if (spawner.groupSpawned && !movedOnce)
        {
            Vector2Int targetCords = positions[0].cords; // first spawned spore in scene regardless of proximity
            Vector2Int startCords = new Vector2Int((int)prefab.transform.position.x, (int)prefab.transform.position.y) / gridManager.UnityGridSize;
            // current position
            
            pathFinder.SetNewDestination(startCords, targetCords);
            RecalculatePath(true);

            movedOnce = true;

            //Debug.LogWarning("StartCords = " + startCords);
            //Debug.LogWarning("targetCords = " + targetCords);
        }
        
    }

    private void ExecutableActions(ActionType action)
    {
        
        switch (action)
        {
            case ActionType.Wander:

                // execute instructions
                // set random destination within close range

                break;
            case ActionType.EatDesired:

                // execute instructions
                // set destination to closest desired gameObject
                // Eat(desired);

                break;
            case ActionType.EatClosest:

                // execute instructions
                // set destination to closest gameObject in WillEat list
                // Eat(closest);

                break;
            default: // Idle

                // execute instructions
                // debug.log Idle

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

    void RecalculatePath(bool resetPath)
    {
        Debug.Log("Recalculate path called");
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
            Debug.Log("trying to move unit");

            Vector2 startPosition = prefab.position; 
            Vector2 endPosition = gridManager.GetPositionFromCoordinates(path[i].cords); 
            float travelPercent = 0f;

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * 2f;
                prefab.position = Vector2.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
