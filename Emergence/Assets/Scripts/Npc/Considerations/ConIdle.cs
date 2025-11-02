using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConIdle", menuName = "UtilityAI/Considerations/Idle Con")]

public class ConIdle : Consideration
{
    [SerializeField] private AnimationCurve responseCurve;
    public override float ScoreConsideration(NpcController npc)
    {
        score = responseCurve.Evaluate(Mathf.Clamp01(npc.hunger / 100f)); // constant or fall back option

        return score;
    }
}
