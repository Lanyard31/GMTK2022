using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;
    public float bounceFactor = 3.5f;

    bool drift = false;
    public bool flipping = false;
    bool boostApplied;
    bool wheelChecked;
    bool turningRight;
    bool diceRolled;

    float accelerationInput = 0;
    float steeringInput = 0;

    public float rotationAngle = 0f;

    float velocityVsUp = 0;
    float driftCounter;

    private float rot;
    private float lastrot;

    Rigidbody2D carRigidBody2D;

    public float rotationAngleClamped;
    public float flipThreshold = 1;
    public float flipDuration = 1;
    DiceRoll diceRoll;
    Animator anim;



    void Awake()
    {
        carRigidBody2D = GetComponent<Rigidbody2D>();
        diceRoll = FindObjectOfType<DiceRoll>();
        anim = GetComponent<Animator>();

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        CheckDrift();

        CheckWheel();

        CheckFlip();

        ApplySteering();
    }

    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, carRigidBody2D.velocity);

        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
            return;

        if (carRigidBody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        if (accelerationInput == 0)
            carRigidBody2D.drag = Mathf.Lerp(carRigidBody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        else carRigidBody2D.drag = 0;


        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        carRigidBody2D.AddForce(engineForceVector, ForceMode2D.Force);

    }

    void ApplySteering()
    {
        float minSpeedBeforeAllowTurningFactor = (carRigidBody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        if (flipping == true)
            return;

        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;



        // Calculate Clamped Rotation
        if (rotationAngle > 360)
        {
            rotationAngle = rotationAngle - 360;
        }

        if (rotationAngle < 0)
        {
            rotationAngle = rotationAngle + 360;
        }



        carRigidBody2D.MoveRotation(rotationAngle);

    }

    void KillOrthogonalVelocity()
    {
        if (flipping == false)
        {
            Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidBody2D.velocity, transform.up);
            Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidBody2D.velocity, transform.right);

            carRigidBody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
        }
    }

    public void SetInputVector(Vector2 InputVector)
    {
        steeringInput = InputVector.x;
        accelerationInput = InputVector.y;
    }


    public void Drift()
    {
        
        if (carRigidBody2D.velocity.magnitude > 4f)
        {
            drift = true;
        }
        
    }

    public void DriftOff()
    {
        drift = false;
    }

    public void CheckDrift()
    {
        if (drift == true)
        {
            turnFactor = 8;
            driftCounter = driftCounter + Time.fixedDeltaTime;
            if (driftCounter > flipThreshold)
            {
                flipping = true;
                //Debug.Log("Currently Flipping");
                // Play Flipping Animation here, at end of animation, set Flipping to False, and call DiceRoll.RollNew
                Invoke("EndFlip", flipDuration);

            }
        }

        if (drift == false)
        {
            driftCounter = 0f;
            turnFactor = 3.5f;
        }

    }

    public void CheckWheel()
    {
        if (lastrot > rot)
        {
            turningRight = true;
        }
        if (lastrot < rot)
        {
            turningRight = false;
        }
        lastrot = rot;
    }

    public void CheckFlip()
    {
        if (flipping == false)
        {

            //ApplyFlipBoost();
        }
    }



    public void EndFlip()
    {
        diceRoll.RollNew();
        flipping = false;
        drift = false;
        boostApplied = false;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject);
        // Debug-draw all contact points and normals
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.red);
            Vector2 bounceVector = contact.normal;
            bounceVector.x = contact.normal.x * bounceFactor;
            bounceVector.y = contact.normal.y * bounceFactor;
            carRigidBody2D.AddForce(bounceVector, ForceMode2D.Impulse);

        }
    }


}