using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProximityToDesired", menuName = "UtilityAI/Considerations/Proximity To Desired")]
public class ProximityToDesired : Consideration
{
    [SerializeField] private AnimationCurve responseCurve;
    public override float ScoreConsideration(NpcController npc)
    {
        score = responseCurve.Evaluate(Mathf.Clamp01(npc.tempDesired / 10f)); // shouldn't be a response curve
        return score;
    }
}
