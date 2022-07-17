using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDeco : MonoBehaviour
{

    public Sprite[] spritelist;
    SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spritelist[Random.RandomRange(0, 32)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
