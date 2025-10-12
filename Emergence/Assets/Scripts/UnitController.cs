using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    // Following "Unity Grid Based Movement System: Part 1 Setup" by StringCode 
    // Following "Unity Grid Based Movement System: Part 2 Breadth First Search" by StringCode

    // change unit controller for indavidual use
    // will become action exacutor as dictated by the utility script

    // will dictate what happens when a prefab is distroyed and when it is spawned(i.e node.walkable = false)

    [SerializeField] float movementSpeed = 1.0f;
    
    Transform selectedUnit;
    bool unitSelected = false;

    List<GridNode> path = new List<GridNode>();

    GridManager gridManager;
    GridPathfinding pathFinder;

    
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<GridPathfinding>(); //change to GetComponent of Type for Individual use
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool hasHit = Physics.Raycast(ray, out hit);

            if(hasHit)
            {
                if(hit.transform.tag == "Tile")
                {
                    if(unitSelected)
                    {
                        Vector2Int targetCords = hit.transform.GetComponent<Tile>().cords; // by clicking get coordinates of the tile and set as the target
                        Vector2Int startCords = new Vector2Int((int)selectedUnit.transform.position.x, (int) selectedUnit.transform.position.y) / gridManager.UnityGridSize;
                        // by clicking a unit, select it and set tile as start 
                        // change target cords to closest prefab of type
                        // change start cords to cords to attached prefab

                        pathFinder.SetNewDestination(startCords, targetCords);
                        RecalculatePath(true);

                        Debug.Log("Tring to Set new destination");
                    }
                }

                if (hit.transform.tag == "Unit")
                {
                    selectedUnit = hit.transform;
                    unitSelected = true;
                    Debug.Log("unit selected");
                }
            }
        }
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
        Debug.Log("follow path called");
        for (int i = 1; i < path.Count; i++)
        {
            Debug.Log("trying to move unit");

            Vector2 startPosition = selectedUnit.position;  // Changed from Vetcor 3
            Vector2 endPosition = gridManager.GetPositionFromCoordinates(path[i].cords); // Changed from Vetcor 3
            float travelPercent = 0f;

            //selectedUnit.LookAt(endPosition); 
            // not necessary for top down 2d

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * movementSpeed;
                selectedUnit.position = Vector2.Lerp(startPosition, endPosition, travelPercent); // Changed from Vetcor 3
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
