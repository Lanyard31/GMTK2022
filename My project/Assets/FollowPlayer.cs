using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    Camera camera;

    private void Awake()
    {
        camera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }
}











/*
public Transform target;

public float smoothSpeed = 0.125f;
public Vector3 offset;

void FixedUpdate()
{
    Vector3 desiredPosition = target.position + offset;
    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    transform.position = smoothedPosition;

    transform.LookAt(target);
}

}









public Transform player;

// Update is called once per frame
void Update()
{
    Vector3 SmoothMove = new Vector3(0, 1, -5);
    SmoothMove = Vector3.SmoothDamp(SmoothMove);
    transform.position = player.transform.position + SmoothMove; //new Vector3(0, 1, -5);
}
*/

