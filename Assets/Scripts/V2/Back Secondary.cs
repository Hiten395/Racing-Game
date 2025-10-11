using UnityEngine;

public class BackSecondary : MonoBehaviour
{
    [SerializeField] Vector3 offset = new Vector3(-3, 0, 0);

    Rigidbody rigidBody;
    FrontPrimary frontPrimary;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        frontPrimary = FindFirstObjectByType<FrontPrimary>();
    }

    private void Update()
    {
        rigidBody.position = frontPrimary.Pos + frontPrimary.direction * offset;
        rigidBody.rotation = frontPrimary.orientation;
    }
}
