using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShell : MonoBehaviour
{
    Rigidbody2D shellRigidBody2D;
    public float bounceFactor = 3.5f;
    Player player;


    private void Awake()
    {
        player = FindObjectOfType<Player>();
        shellRigidBody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

        //Vector2 RandomVector = new Vector2(Random.Range(-0.07f, 0.07f), 4.2f);
       // shellRigidBody2D.AddRelativeForce(RandomVector, ForceMode2D.Impulse);
        //shellRigidBody2D.rotation = player.rotationAngle;
        //shellRigidBody2D.MoveRotation(player.rotationAngle);
        Invoke("SelfDestruct", 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug-draw all contact points and normals
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.red);
            Vector2 bounceVector = contact.normal;
            bounceVector.x = contact.normal.x * bounceFactor;
            bounceVector.y = contact.normal.y * bounceFactor;
            shellRigidBody2D.AddForce(bounceVector, ForceMode2D.Impulse);



            if (collision.transform.tag == "Jelly")
            {
                var collider = collision.gameObject.GetComponent<Jelly>();
                collider.TakeDamage();
                Destroy(gameObject);
            }
            if (collision.transform.tag == "Wyrm")
            {
                var collider = collision.gameObject.GetComponent<Wyrm>();
                collider.TakeDamage();
                Destroy(gameObject);
            }
            if (collision.transform.tag == "Proj")
            {
                var collider = collision.gameObject.GetComponent<enemyProjectile>();
                collider.TakeDamage();
                Destroy(gameObject);
            }
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

}
