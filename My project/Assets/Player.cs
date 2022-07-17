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
    public float flipBoost = 4.5f;

    int maxHealth = 100;
    int playerHP = 100;
    playerHealthBar _playerHealthBar;
    int numberOfBalls = 0;
    int numberOfShells = 0;

    bool drift = false;
    public bool flipping = false;
    bool boostApplied;
    bool wheelChecked;
    bool turningRight;
    bool diceRolled;
    bool flipReset;
    bool jumpMax;
    bool shielded;
    bool powerSlide;
    bool ballSummoned;
    bool dead;

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
    DiceUI diceUI;

    public string Ability1;
    public string Ability2;

    public GameObject particleShield;
    ParticleSystem particleSystemShield;
    ParticleSystem.EmissionModule particleSystemEmissionModuleShield;

    public GameObject powerSlideGO;
    ParticleSystem particleSystemSlide;
    ParticleSystem.EmissionModule particleSystemEmissionModuleSlide;

    public GameObject wreckerPrefab;
    public GameObject projectilePrefab;
    public GameObject shotgunPosition;
    AudioSource audio;

    public GameObject Nova;

    public GameObject CarSprite;
    public GameObject Flipper;
   



    void Awake()
    {
        carRigidBody2D = GetComponent<Rigidbody2D>();
        diceRoll = FindObjectOfType<DiceRoll>();
        anim = GetComponent<Animator>();
        diceUI = FindObjectOfType<DiceUI>();

        particleSystemShield = particleShield.GetComponent<ParticleSystem>();
        particleSystemEmissionModuleShield = particleSystemShield.emission;

        particleSystemSlide = powerSlideGO.GetComponent<ParticleSystem>();
        particleSystemEmissionModuleSlide = particleSystemSlide.emission;

        _playerHealthBar = FindObjectOfType<playerHealthBar>();

        audio = GetComponent<AudioSource>();

        ShieldsDown();

    }

    void Start()
    {
        particleSystemEmissionModuleSlide.rateOverTime = 0;
    }

    void Update()
    {

        ///DEATH CHECK
        if (playerHP <= 0)
        {
            Instantiate(Nova, this.transform.position, Quaternion.identity);
            dead = true;
            CarSprite.SetActive(false);
            Flipper.SetActive(false);
            this.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);

        }
    }

    private void FixedUpdate()
    {
        if (dead == true)
            return;

        ApplyEngineForce();

        KillOrthogonalVelocity();

        CheckDrift();

        CheckWheel();

        CheckFlip();

        ApplySteering();

        CheckPowerSlide();
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

    float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, carRigidBody2D.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;
        if (flipping == true)
        {
            return false;
        }
        if (accelerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }


        if (Mathf.Abs(GetLateralVelocity()) > 3.5f)
            return true;

        return false;

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

            //PowerSliding
            if (powerSlide == true)
            {
                particleSystemEmissionModuleSlide.rateOverTime = 100;
            }

        }

    }

    public void CheckPowerSlide()
    {
        if (powerSlide == false || drift == false)
        {
            particleSystemEmissionModuleSlide.rateOverTime = 0;
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
            if (steeringInput != 0)
            {
                driftCounter = driftCounter + Time.fixedDeltaTime;
            }

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
        rot = rotationAngle;

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
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (flipping == true)
        {
            //Applies Scaling
            if (transform.localScale.x < 1.20f)
            {
                transform.localScale = transform.localScale * 1.01f;
            }
            if (transform.localScale.x >= 1.20f)
            {
                jumpMax = true;
            }
            if (transform.localScale.x < 1f)
                return;

            if (jumpMax == true)
            {
                transform.localScale = transform.localScale * 0.95f;
            }



                //Applies Flip Boost
                if (turningRight == true && flipReset == false)
            {
                Vector2 vec = carRigidBody2D.velocity;
                vec = new Vector2(vec.y, vec.x * -1.0f).normalized * -flipBoost;
                carRigidBody2D.AddForce(vec, ForceMode2D.Impulse);
                flipReset = true;
            }
            if (turningRight == false && flipReset == false)
            {
                Vector2 vec = carRigidBody2D.velocity;
                vec = new Vector2(vec.y * -1.0f, vec.x).normalized * -flipBoost;
                carRigidBody2D.AddForce(vec, ForceMode2D.Impulse);
                flipReset = true;
            }
           
        }
    }


    public void EndFlip()
    {
        diceRoll.RollNew();
        flipping = false;
        flipReset = false;
        drift = false;
        boostApplied = false;
        jumpMax = false;
        ballSummoned = false;
        DestroyBalls();
        DestroyShells();
        Invoke("AbilityCheck", 0.2f);

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
            carRigidBody2D.AddForce(bounceVector, ForceMode2D.Impulse);
            //PlayerDamage(10);
            audio.Play();
            PlayerDamage(1);




            if (collision.transform.tag == "Jelly")
            {
                var collider = collision.gameObject.GetComponent<Jelly>();
                collider.TakeDamage();
                PlayerDamage(8);
                if (powerSlide == true)
                {
                    collider.TakeDamage();
                }
            }

            if (collision.transform.tag == "Proj")
            {
                var collider = collision.gameObject.GetComponent<enemyProjectile>();
                collider.TakeDamage();
                PlayerDamage(3);
                if (powerSlide == true)
                {
                    collider.TakeDamage();
                }
            }

            if (collision.transform.tag == "Wyrm")
            {
                var collider = collision.gameObject.GetComponent<Wyrm>();
                collider.TakeDamage();
                PlayerDamage(10);
                if (powerSlide == true)
                {
                    collider.TakeDamage();
                }
            }

            if (collision.transform.tag == "Gas")
            {
                var collider = collision.gameObject;
                playerHP = 100;
                _playerHealthBar.UpdateHealthBar(maxHealth, playerHP);
                Destroy(collider);
            }


            // Maybe Add for Walls

        }
    }
    public void PlayerDamage(int damage)
    {
        if (shielded == true)
        {
            return;
        }
        if (drift == true )
        {
            return;
        }
        if (shielded == false && drift == false)
        {
            playerHP -= damage;
        }
        //Debug.Log(playerHP);
        _playerHealthBar.UpdateHealthBar(maxHealth, playerHP);
    }

    public void AbilityCheck()
    {
        float Dice1 = diceRoll.Dice1;
        float Dice2 = diceRoll.Dice2;
        ShieldsDown();
        powerSlide = false;


        if (Dice1 == 1)
        {
            Ability1 = "WreckingBall";
            if (numberOfBalls == 0)
            {
                SummonWrecker();
            }
        }
        if (Dice1 == 2)
        {
            Ability1 = "Shotgun";
            if (numberOfShells == 0)
            {
                FireShotgun();
            }
        }
        if (Dice1 == 3)
        {
            Ability1 = "FlameTrail";
        }
        if (Dice1 == 4)
        {
            Ability1 = "Shield";
            ShieldsUp();
        }
        if (Dice1 == 5)
        {
            Ability1 = "Nova";
        }
        if (Dice1 == 6)
        {
            Ability1 = "PowerSlide";
            powerSlide = true;
        }


        if (Dice2 == 1)
        {
            Ability2 = "WreckingBall";
            if (numberOfBalls == 0)
            {
                SummonWrecker();
            }
        }
        if (Dice2 == 2)
        {
            Ability2 = "Shotgun";
            if (numberOfShells == 0)
            {
                FireShotgun();
            }
        }
        if (Dice2 == 3)
        {
            Ability2 = "FlameTrail";
        }
        if (Dice2 == 4)
        {
            Ability2 = "Shield";
        }
        if (Dice2 == 5)
        {
            Ability2 = "Nova";
        }
        if (Dice2 == 6)
        {
            Ability2 = "PowerSlide";
            powerSlide = true;
        }

        diceUI.SetAbilities();

    }

    public void ShieldsUp()
    {
        shielded = true;
        particleSystemEmissionModuleShield.rateOverTime = 100;

    }

    public void ShieldsDown()
    {
        shielded = false;
        particleSystemEmissionModuleShield.rateOverTime = 0;

    }

    public void SummonWrecker()
    {
        if (ballSummoned == false)
        {
            ballSummoned = true;
            numberOfBalls += 1;
            GameObject Wrecker = new GameObject("Wrecker");
            Vector3 objectPOS = this.transform.position;
            GameObject newGameObject = Instantiate(wreckerPrefab, objectPOS, Quaternion.identity);
        }
    }

    public void DestroyBalls()
    {
        GameObject wrecker = GameObject.FindWithTag("Wrecker");
        Destroy(wrecker);
        numberOfBalls = 0;
    }


    public void FireShotgun()
    {
        if (numberOfShells < 8)
        {
            numberOfShells += 1;
            GameObject Shotgun = new GameObject("Shotgun");
            Vector3 objectPOS = shotgunPosition.transform.position;
            GameObject newGameObject = Instantiate(projectilePrefab, objectPOS, Quaternion.identity);
        }
    }

    public void DestroyShells()
    {
        numberOfShells = 0;
    }


}