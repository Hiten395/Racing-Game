using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using Unity.Netcode.Transports.UTP;

public class SceneLoader : NetworkBehaviour
{
    PlayerData playerData;
    NetworkManager network;
    [SerializeField] GameObject playerSolo;
    [SerializeField] GameObject pausePanel;
    int playerState;

    private void Awake()
    {
        SceneManager.sceneLoaded += SetGame;
    }


    private void SetGame(Scene scene, LoadSceneMode mode)
    {
        playerData = FindFirstObjectByType<PlayerData>();
        network = FindFirstObjectByType<NetworkManager>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerState = playerData.gamestate;

        if (playerState == 0)
        {
            Instantiate(playerSolo, new Vector3(0, 5, 0), Quaternion.identity);
        }

        if (playerState == 1)
        {
            network.StartHost();
        }

        if (playerState == 2)
        {
            var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

            transport.SetConnectionData(playerData.IP, 7777, "0.0.0.0");

            network.StartClient();
        }

        SceneManager.sceneLoaded -= SetGame;

    }

    public void UnPause()
    {
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void EndGame()
    {
        if (playerState == 0)
        {
            SceneManager.LoadScene(0);
        }
        if(playerState == 2)
        {
            network.DisconnectClient(playerData.ID);
            SceneManager.LoadScene(0);
        }
    }
}
