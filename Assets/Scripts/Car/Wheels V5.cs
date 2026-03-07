using UnityEngine;

public class WheelsV5 : MonoBehaviour
{
    public bool steerable;
    public bool motorized;

    [HideInInspector] public WheelCollider wheelCollider;

    [SerializeField] Transform mesh;


    private void Start()
    {
        wheelCollider = gameObject.GetComponent<WheelCollider>();
    }

    private void Update()
    {
        Vector3 pos;
        Quaternion rot;

        wheelCollider.GetWorldPose(out pos, out rot);

        mesh.position = pos;
        mesh.rotation = rot;
    }
}
