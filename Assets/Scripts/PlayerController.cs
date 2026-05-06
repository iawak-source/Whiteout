using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float baseSpeed = 15f;
    [SerializeField] float boostSpeed = 20f;


    InputAction moveAction;
    Rigidbody2D myRigidbody2D;
    Vector2 moveVector;
    SurfaceEffector2D mySurfaceEffector2D;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        myRigidbody2D = GetComponent<Rigidbody2D>();
        mySurfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
    }


    void Update()
    {
        RotatePlayer();
        BoostPlayer();
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
}
