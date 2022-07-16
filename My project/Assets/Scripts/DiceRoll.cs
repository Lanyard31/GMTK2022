using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    public float Dice1;
    public float Dice2;

    bool rollable;

    // Start is called before the first frame update
    void Start()
    {
        rollable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollNew()
    {
        if (rollable == true)
        {
            rollable = false;
            Dice1 = Random.Range(1, 7);
            Dice2 = Random.Range(1, 7);
            Invoke("DiceCooldown", 0.5f);
        }
    }



    public void DiceCooldown()
    {
        rollable = true;
    }
}
