using UnityEngine;
using UnityEngine.InputSystem;

public class CarSolo : MonoBehaviour
{
    [SerializeField] float motorTorque;
    [SerializeField] float brakeTorque;

    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float maxTurn = 30f;

    PlayerData data;

    GameObject pausePanel;

    public float time;
    float xInput;
    float yInput;

    public bool IsDrivable = true;

    WheelsV5[] wheels;

    Rigidbody rigidbody;

    private void Start()
    {
        data = FindFirstObjectByType<PlayerData>();
        rigidbody = GetComponentInParent<Rigidbody>();
        wheels = GetComponentsInChildren<WheelsV5>();
        pausePanel = GameObject.Find("Pause Panel");
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
        var cg = pausePanel.GetComponent<CanvasGroup>();
        if (cg == null) cg = pausePanel.AddComponent<CanvasGroup>();

        cg.alpha = 0f;            
        cg.blocksRaycasts = false; 
        cg.interactable = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update() 
    {
      time += Time.deltaTime;
    }

    private void FixedUpdate()
    {

        if (!IsDrivable) return;

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
                wheel.wheelCollider.steerAngle = xInput * currentSteerRange;
            }

            if (isAccelerating)
            { 
                // Apply torque to motorized wheels
                if (wheel.motorized)
                {
                    wheel.wheelCollider.motorTorque = yInput * currentMotorTorque * 1.5f;
                }
                // Release brakes when accelerating
                wheel.wheelCollider.brakeTorque = 0f;
            }
            else
            {
                // Apply brakes when reversing direction
                wheel.wheelCollider.motorTorque = 0f;
                wheel.wheelCollider.brakeTorque = Mathf.Abs(yInput) * brakeTorque;
            }
        }
    }

    public void Disable()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = 0f;
            wheel.wheelCollider.brakeTorque = Mathf.Abs(yInput) * brakeTorque;
        }
    }
}
