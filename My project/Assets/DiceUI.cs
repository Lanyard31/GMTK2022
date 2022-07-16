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

    public GameObject Ability1;
    public GameObject Ability2;


    SpriteRenderer spriteRenderer1;
    SpriteRenderer spriteRenderer2;

    bool rollOnce = false;
    bool shrunken;

    void Start()
    {
        diceRoll = FindObjectOfType<DiceRoll>();
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        spriteRenderer1 = Ability1.GetComponent<SpriteRenderer>();
        spriteRenderer2 = Ability2.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.flipping == true)
        {
            rollOnce = true;
            spriteRenderer1.enabled = false;
            spriteRenderer2.enabled = false;
            anim.Play("RollLoop");
        }
        if (player.flipping == false && rollOnce == true)
        {
            Invoke("ShowDice", 1f);
            anim.Play("diceRoll");
            rollOnce = false;
        }

        Shrink();
    }

    public void SetAbilities()
    {
        if (player.Ability1 == "WreckingBall")
            spriteRenderer1.sprite = spritelist[0];
        if (player.Ability1 == "Shotgun")
            spriteRenderer1.sprite = spritelist[1];
        if (player.Ability1 == "FlameTrail")
            spriteRenderer1.sprite = spritelist[2];
        if (player.Ability1 == "Shield")
            spriteRenderer1.sprite = spritelist[3];
        if (player.Ability1 == "Nova")
            spriteRenderer1.sprite = spritelist[4];
        if (player.Ability1 == "PowerSlide")
            spriteRenderer1.sprite = spritelist[5];


        if (player.Ability2 == "WreckingBall")
            spriteRenderer2.sprite = spritelist[0];
        if (player.Ability2 == "Shotgun")
            spriteRenderer2.sprite = spritelist[1];
        if (player.Ability2 == "FlameTrail")
            spriteRenderer2.sprite = spritelist[2];
        if (player.Ability2 == "Shield")
            spriteRenderer2.sprite = spritelist[3];
        if (player.Ability2 == "Nova")
            spriteRenderer2.sprite = spritelist[4];
        if (player.Ability2 == "PowerSlide")
            spriteRenderer2.sprite = spritelist[5];


        //Invoke("Scale1", 0.0f);
        //Invoke("Scale2", 0.1f);

    }
    /*

    //Applies Scaling
    public void Scale1()
    {
        Ability1.transform.localScale = Ability1.transform.localScale * 1.20f;
        shrunken = false;
        if (Ability1.transform.localScale.x < 1f)
        {
            Ability1.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            shrunken = true;
            return;
        }
        
        if (Ability1.transform.localScale.x >= 1.21f)
        {
            Ability1.transform.localScale = Ability1.transform.localScale * 0.95f * Time.fixedDeltaTime;
        }
        
    }

    public void Scale2()
    {
        Ability2.transform.localScale = Ability2.transform.localScale * 1.20f;
        shrunken = false;
        if (Ability2.transform.localScale.x < 1f)
        {
            Ability2.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            shrunken = true;
            return;
        }
        /*
        if (Ability2.transform.localScale.x >= 1.21f)
        {
            Ability2.transform.localScale = Ability2.transform.localScale * 0.95f * Time.fixedDeltaTime;
        }
        
    }
    */

    public void Shrink()
    {
        if (Ability1.transform.localScale.x > 0.8f && shrunken == false)
        {
            Ability1.transform.localScale = Ability1.transform.localScale * 0.95f;
        }

        if (Ability2.transform.localScale.y > 0.8f && shrunken == false)
        {
            Ability2.transform.localScale = Ability2.transform.localScale * 0.95f;
        }
    }
    public void ShowDice()
    {
        spriteRenderer1.enabled = true;
        spriteRenderer2.enabled = true;
    }
}
