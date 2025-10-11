using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerV2 : MonoBehaviour
{
    [SerializeField] float accel = 10f;
    [SerializeField] float topSpeed = 50f;
    [SerializeField] float brakes = 20f;
    [SerializeField] float reverseTopSpeed = 10f;
    [SerializeField] float rightTurnConstant = 1f;
    [SerializeField] float leftTurnConstant = -1f;
    [SerializeField] float threshold = 0.7f;

    FrontPrimary frontPrimary;

    private void Start()
    {
        frontPrimary = GetComponentInChildren<FrontPrimary>();
    }

    private void Update()
    {
        //Debug.Log();

        //if (accelerate && rigidBody.linearVelocity.magnitude < topSpeed)
        //{
            //Forward();
        //}
        //if (brake && rigidBody.linearVelocity.magnitude < topSpeed)
        {
            //Reverse();
        }
        //if (right)
        {
            //TurnRight();
        }
        //if (left)
        {
            //TurnLeft();
        }
    }


    public void OnAccelerate(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            frontPrimary.Accelerate();
        }
    }

    public void OnBreak(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            frontPrimary.Brake();
        }
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            frontPrimary.Right();
        }
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            frontPrimary.Left();
        }
    }
}
