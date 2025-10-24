using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    // 
    //[SerializeField] public GameObject spore;
    //[SerializeField] public GameObject creatureA;
    //[SerializeField] public GameObject creatureB;

    public List<NpcController> prefabsInScene = new List<NpcController>();
    //public List<GameObject> PrefabsInScene { get { return prefabsInScene; } }

    NpcController npc;
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
        npc = GetComponent<NpcController>();
        //Debug.Log("prefabs in list = " + prefabsInScene.Count);
        
    }

    void Update()
    {
        //Debug.Log("prefabs in list = " + prefabsInScene.Count);
    }
}
