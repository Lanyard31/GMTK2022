using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShell : MonoBehaviour
{
    Rigidbody2D shellRigidBody2D;
    public float bounceFactor = 3.5f;


    // Start is called before the first frame update
    void Start()
    {
        shellRigidBody2D = GetComponent<Rigidbody2D>();
        Vector2 RandomVector = new Vector2(Random.Range(-0.8f, 0.8f), 0f);
        shellRigidBody2D.AddForce(RandomVector, ForceMode2D.Impulse);
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
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

}
