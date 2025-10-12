using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    Wander,
    EatDesired,
    EatClosest,
    Idle
}

public class Actions : MonoBehaviour
{

    // set destination for the pathfinder
    // execute selected action
    // Basic action list: wander, eat closest, eat desired, runaway

    void Start()
    {

    }

    void Update()
    {

    }

    private void ExecutableActions(ActionType action)
    {
        
        switch (action)
        {
            case ActionType.Wander:

                // execute instructions
                // set random destination within close range

                break;
            case ActionType.EatDesired:

                // execute instructions
                // set destination to closest desired gameObject
                // Eat(desired);

                break;
            case ActionType.EatClosest:

                // execute instructions
                // set destination to closest gameObject in WillEat list
                // Eat(closest);

                break;
            default: // Idle

                // execute instructions
                // debug.log Idle

                break;
        }
    }

    void Eat(GameObject prefab)
    {
        // on collision
            // add nutritional value to hunger status
            // remove collided from map prefab list
            // destroy collided
    }
}
