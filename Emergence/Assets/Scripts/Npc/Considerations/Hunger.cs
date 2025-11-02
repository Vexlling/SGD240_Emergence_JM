using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Hunger", menuName = "UtilityAI/Considerations/Hunger")]
public class Hunger : Consideration
{
    [SerializeField] private AnimationCurve responseCurve;
    public override float ScoreConsideration(NpcController npc)
    {
        score = responseCurve.Evaluate(Mathf.Clamp01(npc.maxHunger / 100f));
        return score;
    }

 
}
