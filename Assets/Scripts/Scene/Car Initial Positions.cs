using UnityEngine;
using Unity.Netcode;

public class CarInitialPositions : NetworkBehaviour
{
    [SerializeField] Vector3[] positions;

    NetworkManager network;

    int i = 0;

    private void ReturnPosition(Car car, ulong id)
    {
        car.PositionClientRpc(positions[i], id);
        i++;
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetInitialPositionsServerRpc(ulong id)
    {
        network = NetworkManager.Singleton;

        NetworkObject networkObject = network.ConnectedClients[id].PlayerObject;

        Car car = networkObject.GetComponent<Car>();

        ReturnPosition(car, id);
    }

}
