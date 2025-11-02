using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(fileName = "ConEatClosest", menuName = "UtilityAI/Considerations/Eat Closest Con")]

public class ConEatClosest : Consideration
{
    [SerializeField] private AnimationCurve hungerVsClosestCurve;

    public override float ScoreConsideration(NpcController npc)
    {
        // take closest approx unit.hscore and translate to percentage

        // if this action is already chosen then + 0.1

        score = hungerVsClosestCurve.Evaluate(Mathf.Clamp01(npc.maxHunger / 100f)); // replace 100f with cloest aprrox percentage

        return score;
    }
}
