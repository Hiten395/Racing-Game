using UnityEngine;
using UnityEngine.InputSystem;

public class WheelV3 : MonoBehaviour
{
    [SerializeField] float forceMagnitude;
    [SerializeField] float turnFactor;
    [SerializeField] float topSpeed;

    Quaternion orientation;
    Quaternion direction;

    Rigidbody rigidbody;

    bool accel = false;
    bool left = false;
    bool right = false;

    int col;

    private void Start()
    {
        direction = Quaternion.LookRotation(new Vector3(0, 0, forceMagnitude));

        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        col++;
    }

    private void OnCollisionExit(Collision collision)
    {
        col--;
    }

    private void Update()
    {
        if (col <= 0) return;

        if (accel)
        {
            Accelerate();
        }
        
            MaintainDirection();
       
        if (left)
        {
            TurnLeft();
        }
        else if(right)
        {
            TurnRight();
        }
    }

    public void Accel(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;

        accel = !accel;
    }

    public void Left(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
    
        left = !left;
    }

    public void Right(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;

        right = !right;
    }

    private void Accelerate()
    {
        Debug.Log(rigidbody.linearVelocity.magnitude);
        if (rigidbody.linearVelocity.magnitude >= topSpeed) return;
        

        rigidbody.AddRelativeForce(direction * Vector3.forward * forceMagnitude);
    }

    private void MaintainDirection()
    {
        float velocity = rigidbody.linearVelocity.magnitude;
        rigidbody.linearVelocity = orientation.normalized * Vector3.forward * velocity;
    }

    private void TurnLeft()
    {
        orientation = rigidbody.rotation * Quaternion.Euler(-turnFactor/3, 0, 0);
        rigidbody.MoveRotation(orientation);
    }

    private void TurnRight()
    {

        orientation = rigidbody.rotation * Quaternion.Euler(turnFactor/3, 0, 0);
        rigidbody.MoveRotation(orientation);
    }
}
