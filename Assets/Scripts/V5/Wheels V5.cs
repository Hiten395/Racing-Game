using UnityEngine;

public class WheelsV5 : MonoBehaviour
{
    public bool steerable;
    public bool motorized;
    public WheelCollider WheelCollider;


    private void Start()
    {
        WheelCollider = GetComponent<WheelCollider>();
    }
}
