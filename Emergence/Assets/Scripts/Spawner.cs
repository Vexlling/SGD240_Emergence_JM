using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.Linq;
using UnityEditor;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject[] prefab;
    [SerializeField] private int maxPopulation;
    [SerializeField] private int initialSpawnSize;
    [SerializeField] private float spawnInterval = 1.0f;


    GridNode node;

    GridManager gridManager;
    private Dictionary<Vector2Int, GridNode> grid;

    private List<GridNode> available = new List<GridNode>();
    private List<GameObject> prefabTotal = new List<GameObject>();
    private bool isSpawning = false;
    private bool groupSpawned = false;


    private List<GridNode> positions = new List<GridNode>();
    public List<GridNode> Positions { get { return positions; } }


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
        InitialSpawn();
    }

    void Update()
    {
        if (!isSpawning && prefabTotal.Count < maxPopulation && groupSpawned)
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

        //available.RemoveAll(node => positions.Contains(node)); 


        int randomIndex = Random.Range(0, available.Count);
        GridNode spawnPosition = available[randomIndex];

        if (!positions.Contains(spawnPosition) && spawnPosition.walkable == true)
        {
            GameObject gameObject = Instantiate(prefab[0], new Vector3Int(spawnPosition.cords.x, spawnPosition.cords.y), Quaternion.identity);

            // spawn under UnitManager in Scene

            prefabTotal.Add(gameObject);
            positions.Add(spawnPosition);
        }
        else SpawnPrefab(); // seems like a bad thing to do, like it could endlessly loop   
    }

    private IEnumerator ContinueSpawning()
    {

        isSpawning = true;

        while (prefabTotal.Count < maxPopulation)
        {
            CheckAvailability();
            SpawnPrefab();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }

    private void CheckAvailability() // refreshes everytime to account for moving units
    {

        available = grid.Values.ToList();

        foreach (GridNode entry in available)
        {
            if (entry.walkable == false)
            {
                available.Remove(node);
               // Debug.Log("node removed from available");
            }

            continue;
        }
    }

    private void InitialSpawn()
    {

        CheckAvailability();
        
        for (int count = 0; prefabTotal.Count < initialSpawnSize - 1; count++) // - 1 because it kept spawning intitalSpawnSize + 1?
        {
            SpawnPrefab();
        }

        groupSpawned = true;
        return;
    }

    public void RemoveEaten(GridNode node)
    {
        positions.Remove(node);
    }
}
