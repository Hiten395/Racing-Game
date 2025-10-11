using UnityEngine;

public class FrontSecondary : MonoBehaviour
{
    [SerializeField] Vector3 offset = new Vector3(-3, 0, 0);

    Rigidbody rigidBody;
    FrontPrimary frontPrimary;

    public Quaternion slant;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        frontPrimary = FindFirstObjectByType<FrontPrimary>();
    }

    private void Update()
    {
        rigidBody.position = frontPrimary.Pos + (frontPrimary.direction * slant) * offset;
        rigidBody.rotation = frontPrimary.orientation;
    }

    private void OnTriggerEnter(Collider other)
    {
        slant = other.gameObject.transform.rotation;
    }
}
