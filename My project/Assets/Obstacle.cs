using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Sprite[] spritelist;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spritelist[Random.RandomRange(0, spritelist.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
