using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    // Following StringCode's Unity Grid Based Movement System: Part 1 Setup 
    // Following StringCode's Unity Grid Based Movement System: Part 2 Breadth First Search

    [SerializeField] float movementSpeed = 1.0f;
    
    Transform selectedUnit;
    bool unitSelected = false;

    List<GridNode> path = new List<GridNode>();

    GridManager gridManager;
    GridPathfinding pathFinder;

    
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<GridPathfinding>();
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
                        Vector2Int targetCords = hit.transform.GetComponent<Tile>().cords; // Labeller previous
                        Vector2Int startCords = new Vector2Int((int)selectedUnit.transform.position.x, (int) selectedUnit.transform.position.y) / gridManager.UnityGridSize;
                        // Changed from selectedUnit.transform.position.z

                        pathFinder.SetNewDestination(startCords, targetCords);
                        RecalculatePath(true);
                    }
                }

                if (hit.transform.tag == "Unit")
                {
                    selectedUnit = hit.transform;
                    unitSelected = true;
                }
            }
        }
    }

    void RecalculatePath(bool resetPath)
    {
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
        for(int i = 1; i < path.Count; i++)
        {
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
