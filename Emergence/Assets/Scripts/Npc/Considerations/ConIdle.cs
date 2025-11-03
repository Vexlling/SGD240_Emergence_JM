using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConIdle", menuName = "UtilityAI/Considerations/Idle Con")]

public class ConIdle : Consideration
{
    //[SerializeField] private AnimationCurve responseCurve;
    public override float ScoreConsideration(NpcController npc)
    {
        score = 0.1f; // constant or fall back option

        //if (npc.alreadyExecutingIdle && score <= 0.9f) { score += 0.1f; }

        return score;
    }
}
