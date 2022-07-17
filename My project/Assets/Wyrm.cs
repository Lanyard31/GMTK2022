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
    bool cooldown;


    public GameObject projectile;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        AssignHealth = Random.Range(100, 200);
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.UpdateHealthBar(maxHealth, HP);
        player = FindObjectOfType<Player>();


    }

    // Update is called once per frame
    void Update()
    {
            if (HP <= 0)
                Destroy(gameObject);
    }


    private void FixedUpdate()
    {
        Vector2 targetVector = player.transform.position - gameObject.transform.position;

        if (targetVector.x < 0)
        {
            anim.Play("wyrmleft");
        }
        else
        {
            anim.Play("wyrmright");
        }

        if (Vector3.Distance(player.transform.position, gameObject.transform.position) > 15)
        {
            return;
        }
        else
        {
            if (cooldown == false)
            {
                Vector2 spawnpoint = this.transform.position;
                spawnpoint = new Vector2(spawnpoint.x, spawnpoint.y + 0.5f);

                cooldown = true;
                Instantiate(projectile, spawnpoint, Quaternion.identity);
                Invoke("CoolDownReset", 3f);
            }
        }
    }

    public void CoolDownReset()
    {
        cooldown = false;
    }

    public void TakeDamage()
    {
        Debug.Log("Took Damage");
        HP -= 50;
        healthBar.UpdateHealthBar(maxHealth, HP);
    }

}
