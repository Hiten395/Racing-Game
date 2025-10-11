using UnityEngine;

public class Cameraroot : MonoBehaviour
{
    GameObject body;

    private void Start()
    {
        body = GameObject.Find("Wheel");
    }

    private void Update()
    {
        gameObject.transform.position = body.transform.position;
    }
}

