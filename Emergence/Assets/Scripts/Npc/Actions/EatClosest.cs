using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EatClosest", menuName = "UtilityAI/Actions/Eat Closest")] 
public class EatClosest : Action
{
    public override void Execute(NpcController npc)
    {
        //

        Debug.Log("I'm going to closest");

        // execute action here
        npc.DoEatClosest(3);
  


        //npc.OnFinishedAction(); // might be doubling  up
    }
}
