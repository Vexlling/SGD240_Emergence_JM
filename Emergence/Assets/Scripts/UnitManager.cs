using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class UnitManager : MonoBehaviour
{
    // should the PrefabType enum be here since there will only ever be one UnitManager?


    // GameObject list of Spores, A & B
    public List<Unit> prefabsInScene = new List<Unit>();

    //private List<Unit> prefabsInScene = new List<Unit>();
    //public List<Unit> PrefabsInScene { get { return prefabsInScene; } }

    NpcController npc;
    Unit unit;
    //Unit newUnit;
    Spawner spawner;

    //List<GridNode> positions = new List<GridNode>();

    // if a spore is eaten remove from 
    // RemoveEaten(GridNode node);


    void Start()
    {
        unit = GetComponent<Unit>();
        npc = GetComponent<NpcController>();
        //Debug.Log("prefabs in list = " + prefabsInScene.Count);   
        
    }

    void Update()
    {
        //Debug.Log("prefabs in list = " + prefabsInScene.Count);

        UpdateHCost();
    }
    private void UpdateHCost()
    {
        //for each connection in prefabs
        // if parent location dosen't equal previouse parent location then
        // re calculate connection hCost

        // continue
    }

    public void AddConnection(Unit newUnit)
    {
        // when called add the unit to private prefabs in scene list
        // establish a connection to all existing units in list

        if (prefabsInScene.Count > 0)
        {
            foreach (Unit entry in prefabsInScene)
            {
                //int hCost = (Math.Abs(newUnit.location.x - entry.location.x) + Math.Abs(newUnit.location.y - entry.location.y)) * 10;
                int hCost = GetHCost(newUnit, entry);
                newUnit.connections.Add(entry, hCost);
                if (!entry.connections.ContainsKey(newUnit))
                {
                    entry.connections.Add(newUnit, hCost);
                }

                continue;
            }
        }

        prefabsInScene.Add(newUnit);

        Debug.Log("location = " + newUnit.location + "prefabs in list = " + prefabsInScene.Count);
    }
    public int GetHCost(Unit A, Unit B)
    {
        int hValue = (Math.Abs(A.location.x - B.location.x) + Math.Abs(A.location.y - B.location.y)) * 10;

        return hValue;
    }

    // for each unit in list 
    // add connection to newUnit

    

    public void RemoveConnection(Unit deadUnit)
    {
        // when called remove unit from prefabs list
        // which in theory should severe connections too?
        
        // for each connection in deadUnit
        // remove connection

    }
}

// connection should hold end A & end B + the shared hCost