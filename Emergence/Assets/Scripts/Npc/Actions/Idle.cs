using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle", menuName = "UtilityAI/Actions/Idle")]
public class Idle : Action
{
    public override void Execute(NpcController npc)
    {
        // execute action here
        npc.DoIdle(1);

        // default fall back option
    }
}
