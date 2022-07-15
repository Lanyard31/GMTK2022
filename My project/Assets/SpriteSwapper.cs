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
        //player = FindObjectOfType<Player>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        quat = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType<Player>();
        rot = player.rotationAngle;
        if (rot > 0 && rot < 15)
        {
            spriteRenderer.sprite = spritelist[0];
        }
        if (rot > 15 && rot <= 45)
        {
            spriteRenderer.sprite = spritelist[1];
        }
        if (rot > 45 && rot <= 75)
        {
            spriteRenderer.sprite = spritelist[2];
        }
        if (rot > 75 && rot <= 105)
        {
            spriteRenderer.sprite = spritelist[3];
        }
        if (rot > 105 && rot <= 135)
        {
            spriteRenderer.sprite = spritelist[4];
        }
        if (rot > 135 && rot <= 165)
        {
            spriteRenderer.sprite = spritelist[5];
        }
        if (rot > 165 && rot <= 195)
        {
            spriteRenderer.sprite = spritelist[6];
        }
        if (rot > 195 && rot <= 225)
        {
            spriteRenderer.sprite = spritelist[7];
        }
        if (rot > 225 && rot <= 255)
        {
            spriteRenderer.sprite = spritelist[8];
        }
        if (rot > 255 && rot <= 285)
        {
            spriteRenderer.sprite = spritelist[9];
        }
        if (rot > 285 && rot <= 315)
        {
            spriteRenderer.sprite = spritelist[10];
        }
        if (rot > 315 && rot <= 345)
        {
            spriteRenderer.sprite = spritelist[11];
        }
        if (rot > 345 && rot <= 360)
        {
            spriteRenderer.sprite = spritelist[0];
        }

    }

    private void LateUpdate()
    {
        transform.rotation = quat;
    }
}
