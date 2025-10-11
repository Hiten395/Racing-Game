using UnityEngine;

public class WheelV4 : MonoBehaviour
{
    [SerializeField] Transform mesh;
    new WheelCollider collider;

    private void Start()
    {
        collider = GetComponentInChildren<WheelCollider>();
    }

    public void Accelarate(float power)
    {
        collider.motorTorque = power;
        collider.brakeTorque = 0;
    }

    public void Slow(float brake)
    {
        collider.motorTorque = 0;
        collider.brakeTorque = brake;
    }

    private void FixedUpdate()
    {
        //Debug.Log(collider.isGrounded);
        SetPos();
    }

    private void SetPos()
    {
        Vector3 pos;
        Quaternion quat;

        collider.GetWorldPose(out pos, out quat);

        mesh.position = pos;
        mesh.rotation = quat;

    }
}
