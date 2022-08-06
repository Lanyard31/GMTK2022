using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    Vector2 targetVector;
    Player player;
    Rigidbody2D projRigid;
    int HP = 1;


    // Start is called before the first frame update
    void Start()
    {
        projRigid = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();

        Vector2 targetVector = player.transform.position - gameObject.transform.position;
        targetVector = targetVector.normalized;
        projRigid.AddForce(targetVector * 3.6f, ForceMode2D.Impulse);

        Invoke("SelfDestruct", 4f);


    }

    void Update()
    {
        if (HP <= 0)
            Destroy(gameObject);

    }


    private void FixedUpdate()
    {

    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

    public void TakeDamage()
    {
        HP -= 50;
    }

}
