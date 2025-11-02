using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EatDesired", menuName = "UtilityAI/Actions/Eat Desired")]
public class EatDesired : Action
{
    
    public override void Execute(NpcController npc)
    {
        // execute action here
        npc.DoEatDesired(1);
        npc.maxHunger -= 1;
        Debug.Log("Just Ate 1 desired");
        // logic for updating everything involved with eating

        // takes in hscore for Desired + Closest and evaluates the difference in distance
        // taking into account the hunger response curve to calculate target

        npc.OnFinishedAction();
    }
}
