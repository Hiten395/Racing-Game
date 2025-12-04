using UnityEngine;
using Unity.Netcode;

public class FinishLine : NetworkBehaviour
{
    [SerializeField] Leaderboard leaderboard;

    PlayerData playerData;
    int currentPos = 1;
    int pos;
    float time;

    private void Start()
    {
        playerData = FindAnyObjectByType<PlayerData>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsOwner == false) { return; }

        if (other.gameObject.tag == "Player")
        {
            try
            {
                Car car = other.transform.parent.gameObject.GetComponent<Car>();
                time = car.time;
                car.IsDrivable = false;
                car.Disable();
            }
            catch
            {
                CarSolo car = other.transform.parent.gameObject.GetComponent<CarSolo>();
                time = car.time;
                car.IsDrivable = false;
                car.Disable();
            }
            playerData.xp += 50;
            name = playerData.name;
            pos = currentPos;
            currentPos += 1;
            ServerRPC(pos, name, time);
        }

    }

    [ServerRpc(RequireOwnership = false)]
    void ServerRPC(int a, string b, float c)
    {
        ClientRPC(a, b, c);
    }

    [ClientRpc]
    void ClientRPC(int a, string b, float c)
    {
        leaderboard.Add(b, a, c);
    }
}
