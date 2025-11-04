using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using UnityEditor;

public class Spawner : MonoBehaviour
{

    // Script only really intended for Spores

    // Adjustable variables
    [SerializeField] private GameObject[] prefab;
    [SerializeField] private int maxPopulation;
    [SerializeField] private int initialSpawnSize;
    [SerializeField] private float spawnInterval = 1.0f;


    // Grid System refs
    GridNode node;
    GridManager gridManager;
    private Dictionary<Vector2Int, GridNode> grid;


    // for instantiating spores
    private List<GridNode> available = new List<GridNode>(); // non blocked tiles
    private List<GameObject> prefabTotal = new List<GameObject>(); 
    private bool isSpawning = false;

    public bool groupSpawned { get; private set; }


    private List<GridNode> positions = new List<GridNode>(); // list of instantiated prefabs' locations
    [SerializeField] private GameObject mother; // manual link for getting children to spawn under SceneManager


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
        groupSpawned = false;
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

        // get random position for instantiating
        int randomIndex = Random.Range(0, available.Count);
        GridNode spawnPosition = available[randomIndex];

        if (!positions.Contains(spawnPosition) && spawnPosition.walkable == true)
        {
            GameObject gameObject = Instantiate(prefab[0], new Vector3Int(spawnPosition.cords.x, spawnPosition.cords.y), Quaternion.identity, mother.transform);

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

        //Debug.Log("prefabs in list = " + unitManager.prefabsInScene.Count);

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

    public void RemoveEaten(Vector2Int location) // more universal to use Vector2Int when calling from other scripts
    {
        // Convert to GridNode
        node = gridManager.GetNode(location);

        if (positions.Contains(node))
        {
            positions.Remove(node);
        } 
        else { Debug.Log("node to remove not in positions list"); }
    }
}
