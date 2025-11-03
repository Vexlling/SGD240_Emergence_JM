using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EatClosest", menuName = "UtilityAI/Actions/Eat Closest")] 
public class EatClosest : Action
{
    public override void Execute(NpcController npc)
    {

        Debug.Log("Going to closest");

        // execute action here
        npc.DoEatClosest(1);
        //npc.maxHunger -= 10;
        Debug.Log("Just Ate 1 closest");
        // logic for updating everything involved with eating desired

        //npc.thisUnit.hunger = 0;

        // set target cords to that of the proxDesired unit

        // on collision with target deal 1 damage point, take nutritional value and add to hunger
        // * target being dealt damage either dies off or takes a hit to health

        npc.OnFinishedAction();
    }
}
