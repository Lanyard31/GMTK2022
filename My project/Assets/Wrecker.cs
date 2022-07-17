using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrecker : MonoBehaviour
{

    private LineRenderer lineRenderer;
    public float bounceFactor = 3.5f;
    Rigidbody2D wreckerRigidBody2D;

    // Assign this in Inspector
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        InitLine();
        wreckerRigidBody2D = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPositions(new Vector3[2] { this.transform.position, target.transform.position });



    }

    void InitLine()
    {
        // Get the LineRenderer assigned to the line object.
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.startColor = new Color(1, 1, 1, 1);
        lineRenderer.endColor = new Color(0, 0, 0, 255);
        lineRenderer.positionCount = 2;
    }

    private void FixedUpdate()
    {

        if (Vector3.Distance(target.transform.position, gameObject.transform.position) < 6.5)
        {
            return;
        }
        else
        {
            Vector2 bounceVector = target.transform.position - gameObject.transform.position;
            //bounceVector = (target.transform.position, gameObject.transform.position);
            wreckerRigidBody2D.AddForce(bounceVector, ForceMode2D.Impulse);
        }

        
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
            wreckerRigidBody2D.AddForce(bounceVector, ForceMode2D.Impulse);



            if (collision.transform.tag == "Jelly")
            {
                var collider = collision.gameObject.GetComponent<Jelly>();
                collider.TakeDamage();
            }

        }
    }

}
