using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EatDesired", menuName = "UtilityAI/Actions/Eat Desired")]
public class EatDesired : Action
{
    public override void Execute(NpcController npc)
    {

        // Main logic in Npc controller

        Debug.Log("I'm going to desired");


        npc.DoEatDesired(3);
    }
}
