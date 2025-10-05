using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{

    //Spawner Script will call gridmanager script for walkable nodes and randomly spawn prefabs

    [SerializeField] private int maxPopulation;
    [SerializeField] private int initialSpawnSize;
    [SerializeField] private float spawnInterval = 1.0f;

    // track available grid tiles
    // update per spawnrate tick



    void Start()
    {
        
    }


    void Update()
    {
        
    }

}
