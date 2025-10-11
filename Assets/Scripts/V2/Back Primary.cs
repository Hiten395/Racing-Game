using UnityEngine;

public class BackPrimary : MonoBehaviour
{
    [SerializeField] Vector3 offset = new Vector3(-3, 0, 0);

    Rigidbody rigidBody;
    FrontPrimary frontPrimary;

    public Vector3 Pos;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        frontPrimary = FindFirstObjectByType<FrontPrimary>();
    }

    private void Update()
    {
        Pos = frontPrimary.Pos + frontPrimary.direction * offset;
        rigidBody.position = Pos;
        rigidBody.rotation = frontPrimary.orientation;
    }
}
