using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text speed;

    Rigidbody rigidbody;

    public void getCar(Car car)
    { 
        rigidbody = car.gameObject.GetComponent<Rigidbody>();
    }

    public void getCarSolo(CarSolo carsolo)
    {
        rigidbody = carsolo.gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        speed.text = Mathf.RoundToInt(rigidbody.linearVelocity.magnitude).ToString("D3");
    }

}
