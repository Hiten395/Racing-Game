using UnityEngine;
using UnityEngine.InputSystem;

public class CarSolo : MonoBehaviour
{
    [SerializeField] float motorTorque;
    [SerializeField] float brakeTorque;

    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float maxTurn = 30f;

    GameObject pausePanel;

    float xInput;
    float yInput;

    WheelsV5[] wheels;

    Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
        wheels = GetComponentsInChildren<WheelsV5>();
        pausePanel = GameObject.Find("Pause Panel");
        pausePanel.SetActive(false);
        GameUIManager UI = FindFirstObjectByType<GameUIManager>();
        UI.getCarSolo(this);
    }

    public void Input(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        xInput = input.x;
        yInput = input.y;
    }

    public void Pause(InputAction.CallbackContext context)
    { 
        pausePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void FixedUpdate()
    {

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
}
