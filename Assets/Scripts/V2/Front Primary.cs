using UnityEngine;

public class FrontPrimary : MonoBehaviour
{
    [SerializeField] float maxSpeed = 50f;
    [SerializeField] float velocity = 0f;
    [SerializeField] float naturalSlow = 0.1f;
    [SerializeField] float brakes = 1f;
    [SerializeField] float gravity = 10f;

    public Rigidbody rigidBody;

    bool accel = false;
    bool brake = false;
    bool right = false;
    bool left = false;

    public Vector3 Pos;
    public Quaternion orientation;
    public Quaternion direction;
    public Quaternion rightTurnFactor;
    public Quaternion leftTurnFactor;
    public Quaternion slant;


    public int collision = 0;


    float offset = 0f;
    float effgravity;

    private void Start()
    {
        rigidBody = GetComponentInChildren<Rigidbody>();
        direction = Quaternion.Euler(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        effgravity = 0;
        collision += 1;
        offset = other.gameObject.transform.position.y - rigidBody.position.y + 0.7f;

        slant = other.gameObject.transform.rotation;

        Debug.DrawRay(rigidBody.position, direction * slant * Vector3.forward, Color.red, 1000f);
        Debug.Log(other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        collision -= 1;
    }


    private void FixedUpdate()
    {
        //Debug.Log(collision);
        orientation = rigidBody.rotation;

        if (accel && collision > 0)
        {
            if (right && !left)
            {
                float temp = Mathf.Abs(2 - 4 * Mathf.Clamp(rigidBody.linearVelocity.magnitude / (maxSpeed + 20), 0, 1));
                rightTurnFactor = Quaternion.Euler(rightTurnFactor.x, 2 - temp, rightTurnFactor.z);

                direction *= rightTurnFactor;
            }

            if (left && !right)
            {
                float temp = Mathf.Abs(2 - 4 * Mathf.Clamp(rigidBody.linearVelocity.magnitude / (maxSpeed + 20), 0, 1));
                leftTurnFactor = Quaternion.Euler(leftTurnFactor.x, temp - 2, leftTurnFactor.z);

                direction *= leftTurnFactor;
            }

            Vector3 newPos = new Vector3(rigidBody.position.x, rigidBody.position.y + offset, rigidBody.position.z) + (direction * slant).normalized * new Vector3(0, 0, velocity);
            offset = 0f;
            rigidBody.rotation = (direction * slant).normalized * Quaternion.Euler(0, 0, 90);

            rigidBody.MovePosition(newPos);
            Pos = newPos;
      
            if (rigidBody.linearVelocity.magnitude < maxSpeed)
            {
                velocity += 0.1f;
            }
        }
        else
        {
            if (right && !left && collision > 0)
            {
                float temp = Mathf.Abs(2 - 4 * Mathf.Clamp(rigidBody.linearVelocity.magnitude / (maxSpeed + 20), 0, 1));   
                rightTurnFactor = Quaternion.Euler(rightTurnFactor.x, 2 - temp, rightTurnFactor.z);

                direction *= rightTurnFactor;
            }

            if (left && !right && collision > 0)
            {
                float temp = Mathf.Abs(2 - 4 * Mathf.Clamp(rigidBody.linearVelocity.magnitude / (maxSpeed + 20), 0, 1));
                leftTurnFactor = Quaternion.Euler(leftTurnFactor.x, temp - 2, leftTurnFactor.z);

                direction *= leftTurnFactor;
            }

            Vector3 newPos = new Vector3(rigidBody.position.x, rigidBody.position.y + offset, rigidBody.position.z) + (direction * slant).normalized * new Vector3(0, 0, velocity);
            offset = 0f;
            rigidBody.rotation = (direction * slant).normalized * Quaternion.Euler(0, 0, 90);

            rigidBody.MovePosition(newPos);
            velocity -= naturalSlow;
            Pos = newPos;

            if (velocity < 0)
            {
                velocity = 0;
            }
        }

        if (collision == 0)
        {
            Debug.Log("gravity");
            Vector3 gravityPos =  new Vector3(Pos.x, Pos.y - effgravity, Pos.z);
            effgravity += gravity;
            rigidBody.MovePosition(gravityPos);
            Pos = gravityPos;
        }

        if (brake)
        {
            rigidBody.MovePosition(new Vector3(rigidBody.position.x, rigidBody.position.y, rigidBody.position.z) + direction.normalized * new Vector3(0, 0, velocity));
            velocity -= brakes;
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

    public void Right()
    {
        right = !right;

        if (right)
        {
            //rigidBody.MoveRotation(direction * Quaternion.Euler(0, 30, 90));
        }
        else
        {
            //rigidBody.MoveRotation(Quaternion.Euler(0, 0, 90));
        }
    }

    public void Left()
    {
        left = !left;
    }    
}
