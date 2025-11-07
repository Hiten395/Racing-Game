using UnityEngine;

public class FinishLine : MonoBehaviour
{
    PlayerData playerData;

    private void Start()
    {
        playerData = FindAnyObjectByType<PlayerData>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Car car = other.transform.parent.gameObject.GetComponent<Car>();
            car.IsDrivable = false;
            car.Disable();

            playerData.xp += 50;  
        }
    }
}
