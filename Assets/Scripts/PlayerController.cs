using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float baseSpeed = 15f;
    [SerializeField] float boostSpeed = 20f;
    [SerializeField] ParticleSystem powerupParticle;



    InputAction moveAction;
    Rigidbody2D myRigidbody2D;
    Vector2 moveVector;
    SurfaceEffector2D mySurfaceEffector2D;
    ScoreManager scoreManager;

    public bool canControlPlayer = true;
    float previousRotation;
    float totalRotation;
    int activePowerupCount;
    // float flipCount;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        myRigidbody2D = GetComponent<Rigidbody2D>();
        mySurfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }


    void Update()
    {
        if (canControlPlayer)
        {
            RotatePlayer();
            BoostPlayer();
            CalculateFlip();
        }
    }
    void RotatePlayer()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        if (moveVector.x < 0)
        {
            myRigidbody2D.AddTorque(torqueAmount);
        }
        else if (moveVector.x > 0)
        {
            myRigidbody2D.AddTorque(-torqueAmount);
        }

    }

    void BoostPlayer()
    {
        if (moveVector.y > 0)
        {
            mySurfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            mySurfaceEffector2D.speed = baseSpeed;
        }
    }

    void CalculateFlip()
    {
        float currentRotation = transform.rotation.eulerAngles.z;

        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation);
        if (totalRotation > 340 || totalRotation < -340)
        {
            // flipCount++;
            totalRotation = 0;
            scoreManager.addScore(100);
        }
        previousRotation = currentRotation;
    }

    public void DisableControls()
    {
        canControlPlayer = false;
    }

    public void ActivatePowerup(PowerupSO powerup)
    {
        powerupParticle.Play();
        activePowerupCount += 1;

        if (powerup.GetPowerupType() == "speed")
        {
            baseSpeed += powerup.GetValueChange();
            boostSpeed += powerup.GetValueChange();
        }
        else if (powerup.GetPowerupType() == "torque")
        {
            torqueAmount += powerup.GetValueChange();
        }
    }
    public void DeactivatePowerup(PowerupSO powerup)
    {
        activePowerupCount -= 1;
        if (activePowerupCount <= 0)
        {
            activePowerupCount = 0; // Ensure it doesn't go negative
            powerupParticle.Stop();
        }
        if (powerup.GetPowerupType() == "speed")
        {
            baseSpeed -= powerup.GetValueChange();
            boostSpeed -= powerup.GetValueChange();
        }
        else if (powerup.GetPowerupType() == "torque")
        {
            torqueAmount -= powerup.GetValueChange();
        }
    }
}
