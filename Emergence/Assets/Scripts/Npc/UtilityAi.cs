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
    [SerializeField] private bool randomiseTraits = false;

    [Range(0, 10)] [SerializeField] private int intimidationScale;
    [Range(0, 10)] [SerializeField] private int braveryScale;
   
    private float intimidation;
    private float bravery;


    // action scoring 
    public Action bestAction;
    NpcController npc;
    public bool finishedDeciding;



    void Start()
    {
        TraitSetUp();

        npc = GetComponent<NpcController>();
    }

    void Update()
    {
        if (bestAction is null)
        {
            DecideBestAction(npc.actionsAvailable);
        }
    }

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

    public void DecideBestAction(Action[] actionsAvailable) // needs to be public so other scripts can make use of the function
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

    public float ScoreAction(Action action) // again needs to be public
    {
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



        // input, input normalisation, reponsecurve

        // WANDER
        // input: hunger level
        // response curve: increase, slow at first, fast later

        // EAT CLOSEST
        // input: hunger level & proximity to closest
        // response curve: increase, slow at first, fast later

        // EAT DESIRED
        // input: hunger level & proximity to desired
        // response curve: increase, slow at either end, fast in the middle
    }
}