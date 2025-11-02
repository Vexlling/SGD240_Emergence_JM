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

 

    NpcController npc;
    Unit unit;
    Spawner spawner;

    //List<GridNode> positions = new List<GridNode>();

    // if a spore is eaten remove from 
    // RemoveEaten(GridNode node);


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
  
    }
   


    // Public Function so Queue and PrefabsInScene List can be kept private
    public void AddToQueue(Unit newUnit) //Queue needed because everyone trying to use AddConnection() at once was causing issues.
    {
        newUnitQueue.Enqueue(newUnit);
        Debug.Log("EnQueued");
    }

    private void AddConnection() // the order of lines should make it so newUnits never add themselves to their connections list
    {
        // when called add the unit to prefabs in scene list
        // establish a connection to all existing units in list
        
        Unit newUnit = newUnitQueue.First();
        newUnit.CurrentLocation(); // needs to be here for prefabs starting with the scene, so they don't register as 0,0
        //Debug.Log("location: "+newUnit.location);

        if (prefabsInScene.Count > 0) // so first unit can just be added to the list without the fluff
        {
            foreach (Unit entry in prefabsInScene)
            {

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

        Debug.Log("Co count: " + newUnit.connections.Count);
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

        // unit class will handle the destruction
    }



    public void Eat(Unit preditor, Unit prey)
    {
        // on collision

        preditor.hunger += prey.nValue;
        if (preditor.hunger > 100) { preditor.hunger = 100; } // since 100 is meant to be the max
        // npc.maxhunger

        prey.health -= 1;
        // if prey's health is 0, then unit class will initiate self destruction 
    }
}
