using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceUI : MonoBehaviour
{
    Player player;
    DiceRoll diceRoll;
    Animator anim;
    public Sprite[] spritelist;
    SpriteRenderer spriteRenderer;

    bool rollOnce = false;

    void Start()
    {
        diceRoll = FindObjectOfType<DiceRoll>();
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.flipping == true)
        {
            rollOnce = true;
            anim.Play("RollLoop");
        }
        if (player.flipping == false  && rollOnce == true)
        {
            anim.Play("diceRoll");
            rollOnce = false;
        }
    }

    public void SetAbilities()
    {
        //player.Ability1
    }
}
