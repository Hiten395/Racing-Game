using UnityEngine;

public class WheelsV5 : MonoBehaviour
{
    public bool steerable;
    public bool motorized;
    [HideInInspector] public WheelCollider WheelCollider;


    private void Start()
    {
        WheelCollider = GetComponent<WheelCollider>();
    }
}
