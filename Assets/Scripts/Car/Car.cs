using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class Car : NetworkBehaviour
{
    [SerializeField] float motorTorque;
    [SerializeField] float brakeTorque;

    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float maxTurn = 30f;

    [SerializeField] private Camera camera;

    float xInput;
    float yInput;

    WheelsV5[] wheels;

    Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
        wheels = GetComponentsInChildren<WheelsV5>();
    }

    public void Input(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;

        Vector2 input = context.ReadValue<Vector2>();

        xInput = input.x;
        yInput = input.y;
    }

    private void FixedUpdate()
    {

        if (!IsOwner) return;

        float currentSpeed = Vector3.Dot(transform.forward, rigidbody.linearVelocity);
        float speedFactor = Mathf.Clamp(currentSpeed / maxSpeed, 0, 1);

        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
        float currentSteerRange = Mathf.Lerp(maxTurn, 0, speedFactor);

        bool isAccelerating = Mathf.Sign(yInput) == Mathf.Sign(currentSpeed);

        foreach (var wheel in wheels)
        {
            // Apply steering to wheels that support steering
            if (wheel.steerable)
            {
                wheel.WheelCollider.steerAngle = xInput * currentSteerRange;
            }

            if (isAccelerating)
            {
                // Apply torque to motorized wheels
                if (wheel.motorized)
                {
                    wheel.WheelCollider.motorTorque = yInput * currentMotorTorque * 1.5f;
                }
                // Release brakes when accelerating
                wheel.WheelCollider.brakeTorque = 0f;
            }
            else
            {
                // Apply brakes when reversing direction
                wheel.WheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = Mathf.Abs(yInput) * brakeTorque;
            }
        }
    }

    public void test(InputAction.CallbackContext context)
    {
        
    }

    public override void OnNetworkSpawn()    {
        base.OnNetworkSpawn();
        if (!IsOwner) { return; }
        camera.transform.parent.gameObject.SetActive(true);
    }
}
