using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text speed;

    Rigidbody rigidbody;

    bool status = false;

    public void getCar(Car car)
    { 
        rigidbody = car.gameObject.GetComponent<Rigidbody>();
        status = true;
    }

    public void getCarSolo(CarSolo carsolo)
    {
        rigidbody = carsolo.gameObject.GetComponent<Rigidbody>();
        status = true;
    }

    private void FixedUpdate()
    {
        if (status == false) return;

        speed.text = Mathf.RoundToInt(rigidbody.linearVelocity.magnitude).ToString("D3");
    }

}
