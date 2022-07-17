using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    int AssignHealth;
    int HP = 100;
    HealthBar healthBar;
    int maxHealth = 100;
    Quaternion quat;
    Player player;
    Rigidbody2D jellyRigidBody2D;
    Animator anim;


    public float speed = 2f;
    private float minDistance = 1f;
    private float range = 1f;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.UpdateHealthBar(maxHealth, HP);
        player = FindObjectOfType<Player>();
        quat = transform.rotation;
        AssignHealth = Random.Range(50, 150);
        HP = AssignHealth;
        maxHealth = AssignHealth;
        jellyRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
            Destroy(gameObject);






        //transform.Translate(Vector2.MoveTowards(transform.position, player.transform.position, range) * speed * Time.deltaTime);
    }

    

    public void TakeDamage()
    {
        HP -= 50;
        healthBar.UpdateHealthBar(maxHealth, HP);
    }

    private void FixedUpdate()
    {


        Vector2 bounceVector = player.transform.position - gameObject.transform.position;

        if (bounceVector.y > 0)
        {
            anim.Play("idleleftjelly");
        }
        else
        {
            anim.Play("idlerightjelly");
        }


        if (Vector3.Distance(player.transform.position, gameObject.transform.position) > 15)
        {
            Debug.Log("The Distance between the objects is greater than 15");
            return;
        }


        else
        {
            bounceVector = bounceVector.normalized;
            //bounceVector = (target.transform.position, gameObject.transform.position);
            Debug.Log("The Distance between the objects is more than 15");
            jellyRigidBody2D.AddForce(bounceVector * 0.05f, ForceMode2D.Impulse);
        }
    }

    private void LateUpdate()
    {
        transform.rotation = quat;
    }

}
