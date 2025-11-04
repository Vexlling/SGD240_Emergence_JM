using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consideration : ScriptableObject
{
    // "Parent" class for all consideration scripts

    public string Name;
    private float _score;
    public float score
    {
        get { return _score; }
        set
        {
            this._score = Mathf.Clamp01(value); // so final score will return a value the UtilityAI can use
        }
    }

    public virtual void Awake()
    {
        score = 0;
    }

    public abstract float ScoreConsideration(NpcController npc);
   
}
