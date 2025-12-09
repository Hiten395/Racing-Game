using UnityEngine;
using Unity.Netcode;
using TMPro;
using System.Collections;

public class StartTimer : NetworkBehaviour
{
    [SerializeField] int timer = 20;
    [SerializeField] TMP_Text text;

    bool start = true;

    [ServerRpc(RequireOwnership = false)]
    public void StartTimerServerRPC()
    {
        if (start == false) return;

        start = false;

        text.text = timer.ToString();

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        for (int i = 1; i <= timer; i++)
        {
            yield return new WaitForSeconds(1f);

            UpdateTimerClientRPC(i); 
        }

        Car[] cars = FindObjectsByType<Car>(FindObjectsSortMode.None);

        foreach(Car car in cars)
        {
            car.EnableClientRpc();
        }
    }

    [ClientRpc]
    private void UpdateTimerClientRPC(int i)
    {
        if(i == timer)
        {
            Destroy(text.gameObject);
            return;
        }

        text.text = (timer - i).ToString();
    }
}
