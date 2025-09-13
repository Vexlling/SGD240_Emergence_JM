using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{

    /* Spawner Script will check each tile on the tilemap for available spaces. 
    Then spawn the attached prefabs either overtime or once in the beginning and depending on the amount */


    public Tilemap tilemap;
    //public Tile floor;
    //public Tile obstacle;


    //public float spawnInterval = 0.5f;

    private List<Vector2> validSpawnPositions = new List<Vector2>();

    //
    //public Grid[,] grid;
    
    public Node nodePrefab;
    public List<Node> nodeList;

    public Pathfinder npc;
    private bool canDrawGizmos;
    //

    /*void CreateNodes()
    {
        for(int x = 0; x < grid.GetLength(0); x++)
        {
            for(int y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x, y] == tile.name
            }
        }
    }*/

    void CreateConnections()
    {
        for(int i = 0; i < nodeList.Count; i++)
        {
            for(int j = i + 1; j < nodeList.Count; j++)
            {
                if (Vector2.Distance(nodeList[i].transform.position, nodeList[j].transform.position) <= 1.0f)
                {
                    ConnectNodes(nodeList[i], nodeList[j]);
                    ConnectNodes(nodeList[j], nodeList[i]);
                }
            }
        }
        canDrawGizmos = true;
        SpawnAI();
    }

    void ConnectNodes(Node from, Node to)
    {
        if (from == to) { return; }

        from.connections.Add(to);
    }

    void SpawnAI()
    {
        Node randNode = nodeList[Random.Range(0, nodeList.Count)];

        Pathfinder newNPC = Instantiate(npc, randNode.transform.position, Quaternion.identity);

        newNPC.currentNode = randNode;
    }

    private void OnDrawGizmos()
    {
        if (canDrawGizmos)
        {
            Gizmos.color = Color.blue;
            for(int i = 0;i < nodeList.Count; i++)
            {
                for(int j = 0; j < nodeList[i].connections.Count; j++)
                {
                    Gizmos.DrawLine(nodeList[i].transform.position, nodeList[1].connections[j].transform.position);
                }
            }
        }
    }

    //


    void Start()
    {
        GatherValidPositions(); // create nodes

        //Spawn Objects
    }


    void Update()
    {
        
    }

    private void GatherValidPositions() // Create nodes
    {
        validSpawnPositions.Clear();
        BoundsInt boundsInt = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(boundsInt);
        Vector3 start = tilemap.CellToWorld(new Vector3Int(boundsInt.xMin, boundsInt.yMin, 0));

        for (int x = 0; x < boundsInt.size.x; x++)
        {
            for (int y = 0; x < boundsInt.size.y; y++)
            {
                TileBase tile = allTiles[x + y * boundsInt.size.x];
                if (tile != null)
                {
                    if (tile.name == "BackgroundColour")  
                    {
                        Node node = Instantiate(nodePrefab, new Vector2(x + 0.5f, y + 0.5f), Quaternion.identity);
                        nodeList.Add(node);
                    }
                }
            }
        }
        CreateConnections();
    }
}
