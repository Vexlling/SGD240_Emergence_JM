using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using static UnityEditor.Rendering.CameraUI;
using UnityEngine.Windows;

public class UtilityAi : MonoBehaviour
{

    // personality traits 
    //// hidden from inspector because traits are unable to be taken into consideration scores atm
    [HideInInspector] private bool randomiseTraits = false;

    [Range(0, 10)][HideInInspector] private int intimidationScale;
    [Range(0, 10)][HideInInspector] private int braveryScale;
   
    private float intimidation;
    private float bravery;


    // for actions
    [HideInInspector] public Action bestAction; // shouldn't be tweakable from the inspector
    NpcController npc;
    [HideInInspector] public bool finishedDeciding;

    Spawner spawner;
    Unit thisUnit;

    void Start()
    {
        TraitSetUp();

        npc = GetComponent<NpcController>();
        spawner = GetComponent<Spawner>();
        thisUnit = GetComponent<Unit>();
    }

    void Update()
    {
        if (bestAction is null && thisUnit.connections.Count != 0) // to make sure there are spores in the scene // needed for proximity calc
        {
            DecideBestAction(npc.actionsAvailable);
        }
    }


    //----------------------//
    //      Personality     //
    //----------------------//

    private void TraitSetUp()
    {
        // Converting to percentages for Utility scoring

        if (!randomiseTraits)
        {
            intimidation = (float)intimidationScale / 10;
            bravery = (float)braveryScale / 10;

            //Debug.Log("Set Values = I:" + intimidation + ", B:" + bravery);
        }
        else
        {
            intimidation = Round2Decimals(intimidation, Random.Range(0.0f, 1.0f));
            bravery = Round2Decimals(bravery, Random.Range(0.0f, 1.0f));

            //Debug.Log("Random Values = I:" + intimidation + ", B:" + bravery);    
        }
    }

    private float Round2Decimals(float output, float input)
    {
        output = Mathf.Round(input * 100f) / 100f;
        //Debug.Log("Rounding...");
        return output;
    }


    //-------------------//
    //      Scoring      //
    //-------------------//

    public void DecideBestAction(Action[] actionsAvailable) // needs to be public
    {
        //npc.UpdateHunger();

        float score = 0f;
        int nextBestActionIndex = 0;
        for (int i = 0; i < actionsAvailable.Length; i++)
        {
            if (ScoreAction(actionsAvailable[i]) > score)
            {
                nextBestActionIndex = i;
                score = actionsAvailable[i].score;
            }
        }

        bestAction = actionsAvailable[nextBestActionIndex];

        npc.chosenAction = bestAction.ToString(); 

        finishedDeciding = true;
    }

    public float ScoreAction(Action action) // needs to be public 
    {
        npc.CalculateProximity(); // so considerations can take null into account

        float score = 1f;
        for (int i = 0; i < action.considerations.Length; i++)
        {
            float considerationScore = action.considerations[i].ScoreConsideration(npc);
            score *= considerationScore;

            if (score == 0)
            {
                action.score = 0;
                return action.score; // no point computing further
            }
        }

        // Averaging scheme of overall score
        // dave mark technqiue

        float originalScore = score;
        float modFactor = 1 - (1 / action.considerations.Length);
        float makeupValue = (1 - originalScore) * modFactor;
        action.score = originalScore + (makeupValue * originalScore);

        return action.score;
    }
}