using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] float accelerate = 0f;

    Rigidbody[] rigidbody;

    bool accel = false;
    bool brake = false;

    

    private void Start()
    {
        rigidbody = GetComponentsInChildren<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Debug.Log(rigidbody[0].linearVelocity.magnitude);

        if (accel)
        {
            foreach(Rigidbody rigidBody in rigidbody)
            {
                Debug.Log("a");
                rigidBody.MovePosition(new Vector3(rigidBody.position.x,rigidBody.position.y,rigidBody.position.z + accelerate));
                accelerate += 0.1f;
            }
        }
        else
        {
            accelerate = 0;
        }

        if (brake)
        {
            foreach(Rigidbody rigidBody in rigidbody)
            {
                rigidBody.angularVelocity = rigidBody.angularVelocity + new Vector3(0, 0.1f, 0);
            }
        }
    }

    public void Accelerate()
    {
        accel = !accel;
    }

    public void Brake()
    {
        brake = !brake;
    }
}
