using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EatClosest", menuName = "UtilityAI/Actions/Eat Closest")] 
public class EatClosest : Action
{
    public override void Execute(NpcController npc)
    {

        // Main logic in Npc controller

        Debug.Log("I'm going to closest");


        npc.DoEatClosest(3);
    }
}
