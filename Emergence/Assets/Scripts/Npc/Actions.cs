using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    Idle,
    EatDesired,
    EatClosest
}

public class Actions : MonoBehaviour
{

    // set destination for the pathfinder
    // execute selected action
    // Basic action list: wander, eat closest, eat desired, runaway


    private void ExecutableActions()
    {
        /*
        switch (action)
        {
            case Action.Idle:

                // execute instructions
                // set random destination within close range

                break;
            case Action.EatDesired:

                // execute instructions
                // set destination to closest desired gameObject

                break;
            case Action.EatClosest:

                // execute instructions
                // set destination to closest gameObject in WillEat list

                break;
        }*/
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
