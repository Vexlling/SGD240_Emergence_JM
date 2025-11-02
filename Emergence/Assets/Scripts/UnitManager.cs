using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public enum PrefabType
{
    A,
    B,
    Spore
}

public class UnitManager : MonoBehaviour
{

    // GameObject list of Spores, A & B
    private List<Unit> prefabsInScene = new List<Unit>();

    private Queue<Unit> newUnitQueue = new Queue<Unit>();

    //private List<Unit> prefabsInScene = new List<Unit>();
    //public List<Unit> PrefabsInScene { get { return prefabsInScene; } }

    NpcController npc;
    Unit unit;
    Spawner spawner;

    //List<GridNode> positions = new List<GridNode>();

    // if a spore is eaten remove from 
    // RemoveEaten(GridNode node);

    //int hCost;

    void Start()
    {
        unit = GetComponent<Unit>();
        npc = GetComponent<NpcController>();  
    }

    void Update()
    {
        //Debug.Log("prefabs in list = " + prefabsInScene.Count);

        if (newUnitQueue.Count > 0)
        {
            AddConnection();
        }
        //UpdateHCost();
    }
    /*private void UpdateHCost()
    {
        foreach (Unit unit in prefabsInScene)
        {
            if (unit.connections.Count > 0) 
            {
                foreach (Unit entry in unit.connections)
                {
                    entry.hierarchicalCost = GetHCost(unit, entry);

                }
            }
        }

        //for each connection in prefabs
        // if parent location dosen't equal previouse parent location then
        // re calculate connection hCost
        
        // continue
    }*/


    // Public Function so Queue and PrefabsInScene List can be kept private
    public void AddToQueue(Unit newUnit) //Queue needed because everyone trying to use AddConnection() at once was causing issues.
    {
        newUnitQueue.Enqueue(newUnit);
        Debug.Log("EnQueued");
    }

    private void AddConnection()
    {
        // when called add the unit to prefabs in scene list
        // establish a connection to all existing units in list
        
        Unit newUnit = newUnitQueue.First();

        if (prefabsInScene.Count > 0) // so first unit can just be added to the list without the fluff
        {
            foreach (Unit entry in prefabsInScene)
            {
                // When .connections was a dictionary:
                //hCost = GetHCost(newUnit, entry);
                //newUnit.connections.Add(entry, GetHCost(newUnit, entry));

                newUnit.connections.Add(entry);
                
                if (!entry.connections.Contains(newUnit))
                {
                    entry.connections.Add(newUnit);
                }

                //Debug.Log("NewUnit: " + newUnit.type);
                continue;
            }
        }

        prefabsInScene.Add(newUnit);
        newUnitQueue.Dequeue();
        Debug.Log("DeQueued");

        //Debug.Log("location = " + newUnit.location + "prefabs in list = " + prefabsInScene.Count);
    }

    /*public int GetHCost(Unit A, Unit B)
    {
        int hValue = (Math.Abs(A.location.x - B.location.x) + Math.Abs(A.location.y - B.location.y)) * 10;

        return hValue;
    }*/



    

    public void RemoveConnection(Unit deadUnit)
    {
        // when called remove unit from prefabs list

        foreach (Unit entry in prefabsInScene)
        {
            if (entry.connections.Contains(deadUnit))
            {
                entry.connections.Remove(deadUnit);
            }

            continue;
        }

        prefabsInScene.Remove(deadUnit);

        // destroy prefab here or in another script?
        // can't entirely be done from the npc controller because the spore doesn't have access to it
    }
}
