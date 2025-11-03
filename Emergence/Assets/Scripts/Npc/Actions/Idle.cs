using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle", menuName = "UtilityAI/Actions/Idle")]
public class Idle : Action
{
    public override void Execute(NpcController npc)
    {
        // default fall back option

        Debug.Log("I'm idle");

        npc.DoIdle(5);     
    }
}
