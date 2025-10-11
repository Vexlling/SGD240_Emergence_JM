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

    NpcController npc;


    //tut
    public Action bestAction { get; set; }
    Actions actions;

    // Score Actions function
    // Choose best Action function

    void Start()
    {
        TraitSetUp();

        npc = GetComponent<NpcController>();
    }

    void Update()
    {

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

    public void DecideBestAction(Actions[] actionsAvailable) // might change to private if possible // Actions [] = actions switch cases
    {

    }

    public void ScoreAction(Actions action) // might change to private if possible
    {

    }
}