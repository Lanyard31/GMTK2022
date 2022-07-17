using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    Player player;
    Animator anim;
    SpriteRenderer spriteRenderer;
    float rot;
    Quaternion quat;
    AudioSource audio;


    void Start()
    {
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        quat = transform.rotation;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.flipping == false)
        {
            spriteRenderer.enabled = false;
            anim.enabled = false;
        }
        if (player.flipping == true)
        {
            spriteRenderer.enabled = true;
            anim.enabled = true;
            rot = player.rotationAngle;
            LoopComplete();
            if (rot >= 0 && rot < 90)
                anim.Play("flipUpLeft2");
            if (rot >= 270 && rot < 360)
                anim.Play("flipUpRight2");
            if (rot >= 180 && rot < 270)
                anim.Play("flipDownRight2");
            if (rot >= 90 && rot < 180)
                anim.Play("flipDownLeft2");
        }
    }

    private void LateUpdate()
    {
        transform.rotation = quat;
    }

    public void LoopComplete()
    {
        //audio.Play();
    }
}
