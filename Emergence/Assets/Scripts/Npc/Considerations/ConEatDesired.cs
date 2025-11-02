using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConEatDesired", menuName = "UtilityAI/Considerations/ Eat Desired Con")]

public class ConEatDesired : Consideration
{
    [SerializeField] private AnimationCurve hungerVsDesiredCurve;
    public override float ScoreConsideration(NpcController npc)
    {
        // take desired approx unit.hscore and translate to percentage

        // if this action is already chosen then + 0.1

        score = hungerVsDesiredCurve.Evaluate(Mathf.Clamp01(npc.maxHunger / 100f)); // replace 100f with desired aprrox percentage

        return score;
    }
}
