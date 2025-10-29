using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Trackers : MonoBehaviour
{

    // track state scores
    // track consideration scores

    // CONSIDERATIONS
    // Max hunger
    // time
    // hunger level
    // proximity to closest
    // proximity to desired

    // TRACK
    // hunger level = Max hunger - time
    // proximity to closest =
    // proximity to desird =

    // will need ref to gridManager

    // SCORING
    // input: hunger Status
    // response curve: increase, slow at first, fast later


    //private NpcController npc;
    //private NpcController thisNpc;

    private Unit unit;
    private Unit thisUnit;

    Unit closestProx;
    Unit desiredProx;

    public List<Unit> proximity = new List<Unit>();



    UnitManager unitManager;

    void Start()
    {
        unitManager = GetComponentInParent<UnitManager>(); // not sure this works
        //thisNpc = GetComponent<NpcController>();
        thisUnit = GetComponent<Unit>();

        //List<Unit> proximity = unitManager.prefabsInScene;
    }


    void Update()
    {
        UpdateHunger();
    }

    private void UpdateHunger()
    {

    }

    /*
    public void CalculatePrxoimity() // only needs to be called once per run or per decision
    {
        //List<Unit> proximity = unitManager.prefabsInScene;
        // get reference to public prefabs in scene list

        if (proximity.Contains(thisUnit) )
        {
            proximity.Remove(thisUnit);
        }
        Debug.Log("proximity = " + proximity.Count);
        if (proximity.Count > 0)
        {
            UpdateProximity();
            closestProx = proximity.First();
            //Debug.Log("closestProx = " + closestProx);
        }
        
       // return;
        // this method could run into the error of new prefabs being added to the orginal list
        // which this personal list won't pick up on
        // or even run into the issue of another prefab already being eaten
    }

    public void UpdateProximity() // can be called multiple times
    {
        foreach (Unit entry in proximity)
        {
            entry.hierarchicalCost = (Math.Abs(thisUnit.location.x - unit.location.x) + Math.Abs(thisUnit.location.y - unit.location.y)) * 10;
            //continue;
            Debug.Log("hierarchicalCost = " + entry.hierarchicalCost);
        }
        
        proximity.OrderBy(n => n.hierarchicalCost).ToList(); // ascending?

       // closestProx = proximity.First();
        //Debug.Log("closestProx = " + closestProx);
        
        // closestProx = proximity.FindIndex.Any(thisNpc.willEat);
        if (proximity.Contains(thisNpc.desired)
        {
            desiredProx = proximity.Where(npc => npc.body == thisNpc.desired).First();
        }
        else { desiredProx = null; }

        //thisNpc.willEat
        //thisNpc.desired

        // variable = List[0]
        
    }*/
}
