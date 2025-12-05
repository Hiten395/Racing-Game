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


        try
        {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        }
        catch { }

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

            try
            {
                network.StartClient();
            }
            catch
            {
                SceneManager.LoadScene(0);
            }
        }

        if (playerState == 4)
        {
            network.StartServer();
        }

        SceneManager.sceneLoaded -= SetGame;

    }

    public void UnPause()
    {
        var cg = pausePanel.GetComponent<CanvasGroup>();        if (cg == null) cg = pausePanel.AddComponent<CanvasGroup>();        Debug.Log(cg);        cg.alpha = 0f;             
        cg.blocksRaycasts = false;  
        cg.interactable = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void EndGame()
    {
        if (playerState == 0)
        {
            SceneManager.LoadScene(0);
        }
        if (playerState == 1)
        {
            NetworkManager.Singleton.Shutdown();
            SceneManager.LoadScene(0);
        }
        if(playerState == 2)
        {
            network.DisconnectClient(playerData.ID);
            SceneManager.LoadScene(0);
        }
    }
}
