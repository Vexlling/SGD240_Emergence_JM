using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConEatDesired", menuName = "UtilityAI/Considerations/Eat Desired Con")]

public class ConEatDesired : Consideration
{
    // response curve tweakable from the equivalent ScriptableObject
    [SerializeField] private AnimationCurve hungerVsDesiredCurve;

    public override float ScoreConsideration(NpcController npc)
    {
        // wanted to take desired approx unit.hscore and translate to percentage

        score = hungerVsDesiredCurve.Evaluate(Mathf.Clamp01(npc.maxHunger / 100f)); // wanted to replace 100f with desired aprrox percentage

        //if (npc.alreadyExecutingDesired && score <= 0.9f) { score += 0.1f; }


        // catch if null
        if (npc.desiredProx == null)
        {
            Debug.Log("No desired to eat");
            score = 0f;
        }


        return score;
    }
}
