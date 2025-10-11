using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    // 
    [SerializeField] public GameObject spore;
    [SerializeField] public GameObject creatureA;
    [SerializeField] public GameObject creatureB;

    private List<GameObject> sporeTotal = new List<GameObject>();
    private List<GameObject> aTotal = new List<GameObject>();
    private List<GameObject> bTotal = new List<GameObject>();

    // GameObject list of Spores, A & B

    Spawner spawner;
    //List<GridNode> positions = new List<GridNode>();

    // if a spore is eaten remove from 
    // RemoveEaten(GridNode node);


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
