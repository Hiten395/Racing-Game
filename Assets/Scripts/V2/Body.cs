using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] Vector3 offset = new Vector3(-1.5f, 0, -2f);

    FrontPrimary frontPrimary;
    BackPrimary backPrimary;
    Rigidbody rigidbody;

    Quaternion direction;
    
    void Start()
    {
        frontPrimary = FindFirstObjectByType<FrontPrimary>();
        backPrimary = FindFirstObjectByType<BackPrimary>();
        rigidbody = GetComponent<Rigidbody>();
    }

   
    void Update()
    {
        rigidbody.position = frontPrimary.Pos + frontPrimary.direction * offset;

        Vector3 orientation = frontPrimary.Pos - backPrimary.Pos;
        direction = Quaternion.LookRotation(orientation);

        rigidbody.rotation = direction;
        //rigidbody.isKinematic = frontPrimary.rigidBody.isKinematic;
    }
}
