using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EatClosest", menuName = "UtilityAI/Actions/Eat Closest")] 
public class EatClosest : Action
{
    public override void Execute(NpcController npc)
    {
        // execute action here
        npc.DoEatClosest(1);
        npc.maxHunger -= 10;
        Debug.Log("Just Ate 1 closest");
        // logic for updating everything involved with eating

        // takes in hscore for Desired + Closest and evaluates the difference in distance
        // taking into account the hunger response curve to calculate target

        npc.OnFinishedAction();
    }
}
