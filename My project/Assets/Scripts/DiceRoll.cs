using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    public float Dice1;
    public float Dice2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollNew()
    {
        Dice1 = Random.Range(1, 7);
        Dice2 = Random.Range(1, 7);
    }

}
