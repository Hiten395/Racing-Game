using UnityEngine;
using Unity.Netcode;

public class FinishLine : NetworkBehaviour
{
    [SerializeField] Leaderboard leaderboard;

    PlayerData playerData;

    int currentPos = 1;
    float time;

    private void Start()
    {
        playerData = FindFirstObjectByType<PlayerData>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("function triggered");

        if (IsOwner == false) { return; }

        Debug.Log("server check passed");

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Tag verified");

            if (other.transform.parent.TryGetComponent<NetworkObject>(out NetworkObject networkObject))
            {
                Debug.Log("Network object Found");

                ulong id = networkObject.OwnerClientId;

                FirstClientRPC(id);
            }
            else
            {

            }
        }

    }

    [ClientRpc]
    void FirstClientRPC(ulong id)
    {
        Debug.Log("FirstClientRPC Client RPC triggered");

        if (!(id == NetworkManager.Singleton.LocalClientId)) return;

        Debug.Log("filtering check");

        string name = playerData.name;

        NetworkObject networkObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();

        Car car = networkObject.GetComponent<Car>();
        time = car.time;
        car.Disable();

        ServerRPC(name, time);
    }

    [ServerRpc(RequireOwnership = false)]
    void ServerRPC(string b, float c)
    {
        ClientRPC(currentPos, b, c);
        currentPos++;
    }

    [ClientRpc]
    void ClientRPC(int a, string b, float c)
    {
        leaderboard.Add(b, a, c);
    }
}
