using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProximityToClosest", menuName = "UtilityAI/Considerations/Proximity To Closest")]
public class ProximityToClosest : Consideration
{
    [SerializeField] private AnimationCurve responseCurve;
    
    public override float ScoreConsideration(NpcController npc)
    {

        score = responseCurve.Evaluate(Mathf.Clamp01(npc.tempClosest / npc.maxHunger)); // shouldn't be a response curve
        // score should equal 
        return score;
    }
}
