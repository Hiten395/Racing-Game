using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float accel = 10f;
    [SerializeField] float topSpeed = 50f;
    [SerializeField] float brakes = 20f;
    [SerializeField] float reverseTopSpeed = 10f;
    [SerializeField] float turnConstant = 1f;
    [SerializeField] float threshold = 0.7f;

    Rigidbody rigidBody;

    bool accelerate = false;
    bool brake = false;
    bool right = false;
    bool left = false;

    private void Start()
    {
        rigidBody = transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.Log(rigidBody.linearVelocity.magnitude);

        if (transform.position.y < threshold)
        {
            rigidBody.useGravity = false;
        }
        else
        {
            rigidBody.useGravity = true;
        }

        if (accelerate && rigidBody.linearVelocity.magnitude < topSpeed)
        {
            Forward();
        }
        if (brake && rigidBody.linearVelocity.magnitude < topSpeed)
        {
            Reverse();
        }
        if (right)
        {
            TurnRight();
        }
        if (left)
        {
            TurnLeft();
        }
    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            accelerate = !accelerate;
        }
    }

    public void OnBreak(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            brake = !brake;
        }
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            right = !right;
        }
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            left = !left;
        }
    }


    private void Forward()
    {
        rigidBody.AddRelativeForce(0, 0, accel);
    }

    private void Reverse()
    {
        rigidBody.AddRelativeForce(0, 0, -brakes);
    }

    private void TurnRight()
    {
        rigidBody.AddTorque(new Vector3(0, turnConstant * rigidBody.linearVelocity.magnitude / 50, 0), ForceMode.Force);
    }

    private void TurnLeft()
    {
        rigidBody.AddTorque(new Vector3(0, -turnConstant * rigidBody.linearVelocity.magnitude / 50, 0), ForceMode.Force);
    }
}

