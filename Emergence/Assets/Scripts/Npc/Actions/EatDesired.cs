using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EatDesired", menuName = "UtilityAI/Actions/Eat Desired")]
public class EatDesired : Action
{
    
    public override void Execute(NpcController npc)
    {
        //

        Debug.Log("I'm going to desired");

        // execute action here
        npc.DoEatDesired(3);
        
        

      

        //npc.OnFinishedAction(); // might be doubling  up
    }
}
