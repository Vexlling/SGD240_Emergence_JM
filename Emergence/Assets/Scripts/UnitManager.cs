using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    // 
    //[SerializeField] public GameObject spore;
    //[SerializeField] public GameObject creatureA;
    //[SerializeField] public GameObject creatureB;

    public List<GameObject> prefabsInScene = new List<GameObject>();
    //public List<GameObject> PrefabsInScene { get { return prefabsInScene; } }

    
    //private List<GameObject> aTotal = new List<GameObject>();
    //private List<GameObject> bTotal = new List<GameObject>();

    // GameObject list of Spores, A & B

    [SerializeField] private int sporeNutrition; // make random an option too?
   

    Spawner spawner;
    //List<GridNode> positions = new List<GridNode>();

    // if a spore is eaten remove from 
    // RemoveEaten(GridNode node);


    void Start()
    {
        Debug.Log("prefabs in list = " + prefabsInScene.Count);
    }

    void Update()
    {
        
        
    }
}
