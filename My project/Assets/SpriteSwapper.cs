using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwapper : MonoBehaviour
{
    Player player;
    public float rot;
    SpriteRenderer spriteRenderer;
    public Sprite[] spritelist;
    Quaternion quat;




    void Start()
    {
        player = FindObjectOfType<Player>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        quat = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //player = FindObjectOfType<Player>();
        rot = player.rotationAngle;
        if (player.flipping == true)
            spriteRenderer.enabled = false;
        if (player.flipping == false)
            spriteRenderer.enabled = true;
         

        if (rot > 0 && rot < 11.25)
        {
            spriteRenderer.sprite = spritelist[0];
        }
        if (rot > 11.25 && rot <= 33.75)
        {
            spriteRenderer.sprite = spritelist[1];
        }
        if (rot > 33.75 && rot <= 56.25)
        {
            spriteRenderer.sprite = spritelist[2];
        }
        if (rot > 56.25 && rot <= 78.75)
        {
            spriteRenderer.sprite = spritelist[3];
        }
        if (rot > 78.75 && rot <= 101.25)
        {
            spriteRenderer.sprite = spritelist[4];
        }
        if (rot > 101.25 && rot <= 123.75)
        {
            spriteRenderer.sprite = spritelist[5];
        }
        if (rot > 123.75 && rot <= 146.25)
        {
            spriteRenderer.sprite = spritelist[6];
        }
        if (rot > 146.25 && rot <= 168.75)
        {
            spriteRenderer.sprite = spritelist[7];
        }
        if (rot > 168.75 && rot <= 191.25)
        {
            spriteRenderer.sprite = spritelist[8];
        }
        if (rot > 191.25 && rot <= 213.75)
        {
            spriteRenderer.sprite = spritelist[9];
        }
        if (rot > 213.75 && rot <= 236.25)
        {
            spriteRenderer.sprite = spritelist[10];
        }
        if (rot > 236.25 && rot <= 258.75)
        {
            spriteRenderer.sprite = spritelist[11];
        }
        if (rot > 258.75 && rot <= 281.25)
        {
            spriteRenderer.sprite = spritelist[12];
        }
        if (rot > 281.25 && rot <= 303.75)
        {
            spriteRenderer.sprite = spritelist[13];
        }
        if (rot > 303.75 && rot <= 326.25)
        {
            spriteRenderer.sprite = spritelist[14];
        }
        if (rot > 326.25 && rot <= 348.75)
        {
            spriteRenderer.sprite = spritelist[15];
        }
        if (rot > 348.75 && rot <= 360)
        {
            spriteRenderer.sprite = spritelist[0];
        }

    }

    private void LateUpdate()
    {
        transform.rotation = quat;
    }
}
