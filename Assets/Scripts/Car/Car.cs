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
    [SerializeField] private GameObject pausePanel;

    PlayerData data;

    public float time;
    float xInput;
    float yInput;

    public bool IsDrivable = true;
    bool spawn = true;

    WheelsV5[] wheels;

    Rigidbody rigidbody;

    private void Start()
    {
        data = FindFirstObjectByType<PlayerData>();
        rigidbody = GetComponentInParent<Rigidbody>();
        wheels = GetComponentsInChildren<WheelsV5>();
        pausePanel = GameObject.Find("Pause Panel");


        var cg = pausePanel.GetComponent<CanvasGroup>();
        if (cg == null) cg = pausePanel.AddComponent<CanvasGroup>();

        GameUIManager UI = FindFirstObjectByType<GameUIManager>();
        UI.getCar(this);

        cg.alpha = 0f;
        cg.blocksRaycasts = false;
        cg.interactable = false;

    }

    private void OnEnable()
    {
        
    }

    public void Input(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;

        Vector2 input = context.ReadValue<Vector2>();

        xInput = input.x;
        yInput = input.y;
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;

        var cg = pausePanel.GetComponent<CanvasGroup>();
        if (cg == null) cg = pausePanel.AddComponent<CanvasGroup>();

        cg.alpha = 1f;
        cg.blocksRaycasts = true;
        cg.interactable = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update() 
    {
      time += Time.deltaTime;
    }

    private void FixedUpdate()
    {

        if (!IsOwner || !IsDrivable) return;

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

    public void Disable()
    {
        Debug.Log("Car Disabled");

        foreach (var wheel in wheels)
        {
            wheel.WheelCollider.motorTorque = 0f;
            wheel.WheelCollider.brakeTorque = Mathf.Abs(yInput) * brakeTorque;
        }

        IsDrivable = false;
    }

    public void test(InputAction.CallbackContext context)
    {
        
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsOwner) { return; }

        if (spawn == true)
        {
            NetworkManager.Singleton.OnConnectionEvent += HandleConnection;
            spawn = false;
        }

        camera.transform.parent.gameObject.SetActive(true);

        data = FindFirstObjectByType<PlayerData>();
        data.ID = NetworkManager.Singleton.LocalClientId;
    }

    private void HandleConnection(NetworkManager mgr, ConnectionEventData data)
    { 
        CarInitialPositions carInitialPositions = FindFirstObjectByType<CarInitialPositions>();
        carInitialPositions.SetInitialPositionsServerRpc(NetworkManager.Singleton.LocalClientId);

        StartTimer startTimer = FindFirstObjectByType<StartTimer>();
        startTimer.StartTimerServerRPC();
    }

    [ClientRpc]
    public void PositionClientRpc(Vector3 position, ulong id)
    {
        if(NetworkManager.Singleton.LocalClientId != id)
        {
            return;
        }

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        rigidbody.MovePosition(position);

        NetworkManager.Singleton.OnConnectionEvent -= HandleConnection;
    }

    [ClientRpc]
    public void EnableClientRpc()
    {
        IsDrivable = true;
        time = 0;
    }
}
