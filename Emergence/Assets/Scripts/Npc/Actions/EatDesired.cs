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
        npc.hunger -= 1;
        Debug.Log("Just Ate 1 desired, hunger: "+ npc.hunger);

        // logic for updating everything involved with eating desired

        // set target cords to that of the proxDesired unit

        // on collision with target deal 1 damage point, take nutritional value and add to hunger
        // * target being dealt damage either dies off or takes a hit to health

        npc.OnFinishedAction();
    }
}
