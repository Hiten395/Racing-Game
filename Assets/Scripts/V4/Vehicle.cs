using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    [SerializeField] WheelV4[] wheels;
    [SerializeField] bool w = false;
    [SerializeField] bool a;
    [SerializeField] bool s;
    [SerializeField] bool d;
    [SerializeField] float power;
    [SerializeField] float brake;


    private void FixedUpdate()
    {
        if (w)
        {
            foreach (WheelV4 wheel in wheels)
            {
                wheel.Accelarate(power);
            }    
        }
        else
        {
            foreach (WheelV4 wheel in wheels)
            {
                wheel.Slow(brake);
            }
        }
    }

    public void Accelerate(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;

        w = !w;
    }
}
