using UnityEngine;
using Unity.Netcode;

public class SceneLoader : NetworkBehaviour
{
    PlayerData playerData;
    NetworkManager network;
    int playerState;

    private void Start()
    {
        playerData = FindFirstObjectByType<PlayerData>();
        network = FindFirstObjectByType<NetworkManager>();

        playerState = playerData.gamestate;

        if (playerState == 1)
        {
            network.StartHost();
        }

        if (playerState == 2)
        {
            network.StartClient();
        }

        
    }
}
