using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    int HP = 100;
    HealthBar healthBar;
    int maxHealth = 100;
    Quaternion quat;



    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.UpdateHealthBar(maxHealth, HP);
        quat = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        if (HP <= 0)
            Destroy(gameObject);
    }

    public void TakeDamage()
    {
        HP -= 50;
        healthBar.UpdateHealthBar(maxHealth, HP);
    }

    private void LateUpdate()
    {
        transform.rotation = quat;
    }

}
