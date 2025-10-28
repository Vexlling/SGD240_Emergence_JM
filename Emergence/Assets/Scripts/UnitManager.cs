using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    // should the PrefabType enum be here since there will only ever be one UnitManager?


    // GameObject list of Spores, A & B
    public List<Unit> prefabsInScene = new List<Unit>();
    //public List<GameObject> PrefabsInScene { get { return prefabsInScene; } }

    NpcController npc;
    Unit unit;
    Spawner spawner;

    //List<GridNode> positions = new List<GridNode>();

    // if a spore is eaten remove from 
    // RemoveEaten(GridNode node);


    void Start()
    {
        npc = GetComponent<NpcController>();
        //Debug.Log("prefabs in list = " + prefabsInScene.Count);
        unit = GetComponent<Unit>();
        
    }

    void Update()
    {
        //Debug.Log("prefabs in list = " + prefabsInScene.Count);
    }
}
