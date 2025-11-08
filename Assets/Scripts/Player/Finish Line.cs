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
            try
            {
                Car car = other.transform.parent.gameObject.GetComponent<Car>();
                car.IsDrivable = false;
                car.Disable();
            }
            catch
            {
                CarSolo car = other.transform.parent.gameObject.GetComponent<CarSolo>();
                car.IsDrivable = false;
                car.Disable();
            }
            playerData.xp += 50;  
        }
    }
}
