using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wyrm : MonoBehaviour
{
    int AssignHealth;
    int HP = 100;
    HealthBar healthBar;
    int maxHealth = 100;
    Player player;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        AssignHealth = Random.Range(100, 200);
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.UpdateHealthBar(maxHealth, HP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
