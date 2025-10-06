using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.Linq;

public class Spawner : MonoBehaviour
{

    //Spawner Script will call gridmanager script for walkable nodes and randomly spawn prefabs

    [SerializeField] private GameObject[] prefab;
    [SerializeField] private int maxPopulation;
    [SerializeField] private int initialSpawnSize;
    [SerializeField] private float spawnInterval = 1.0f;

    // track available grid tiles
    // update per spawnrate tick


    GridNode node;

    GridManager gridManager;
    private Dictionary<Vector2Int, GridNode> grid = new Dictionary<Vector2Int, GridNode>();
    
    //List<Vector2Int> tempBlocked = new List<Vector2Int>();
    List<GridNode> available = new List<GridNode>();
    private List<GameObject> prefabTotal = new List<GameObject>();
    private bool isSpawining = false;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
        }
    }

    void Start()
    {
        CheckAvailability();
        StartCoroutine(ContinueSpawning());
    }

    // on spawn intervals
    // add all nodes from tempariiy blocked list to potential list
    // check the dictionary for any nodes not walkable anymore
    // add not walkable nodes to tempuariliy blocked list
    // spawn on random available tiles

    void Update()
    {
        if (!isSpawining && prefabTotal.Count < maxPopulation)
        {
            StartCoroutine(ContinueSpawning());
        }
    }

    private void SpawnPrefab()
    {
        if (available.Count == 0)
        {
            Debug.Log("available is empty");
            return;
        }


        int randomIndex = Random.Range(0, available.Count);
        GridNode spawnPosition = available[randomIndex];

        if (spawnPosition.walkable == true)
        {
            GameObject gameObject = Instantiate(prefab[0], new Vector3Int(spawnPosition.cords.x, spawnPosition.cords.y), Quaternion.identity);
            prefabTotal.Add(gameObject);
        }
        
    }

    private IEnumerator ContinueSpawning()
    {
        isSpawining = true;

        while (prefabTotal.Count < maxPopulation)
        {
            CheckAvailability();
            SpawnPrefab();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawining = false;
    }

    private void CheckAvailability()
    {
        //tempBlocked.Clear();
        available.Clear();

        // available = grid;

        /*foreach (KeyValuePair<Vector2Int, GridNode> entry in grid)
        {
            print("You have " + entry.Value + " " + entry.Key);

        }*/
        available = grid.Values.ToList();
        /*foreach (KeyValuePair<Vector2Int, GridNode> entry in grid)
        {
            //if (node.walkable == true)
            //{
                available.Add(node);
                Debug.Log("node added to available");
            //}

            //continue;
        }*/
        /*foreach (var node in available)
        {
            Debug.Log(node.ToString());
        }*/
    }

    // tile availability
    // get tile blocked bool
    // get tile cords
    // if tile is not blocked
    // add tile cords to dictionary

}
