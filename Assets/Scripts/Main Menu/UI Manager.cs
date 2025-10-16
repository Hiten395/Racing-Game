using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class UIManager : NetworkBehaviour
{
    [SerializeField] GameObject playPanel;
    [SerializeField] GameObject homePanel;
    PlayerData playerdata;

    private void Start()
    {
        playerdata = FindFirstObjectByType<PlayerData>();
    }

    public void Play()
    {
        homePanel.SetActive(false);
        playPanel.SetActive(true);
    }

    public void Cancel()
    {
        homePanel.SetActive(true);
        playPanel.SetActive(false);
    }

    public void Create()
    {
        homePanel.SetActive(false);
        playPanel.SetActive(false);
        playerdata.gamestate = 1;
        SceneManager.LoadScene(1);
    }

    public void Join()
    {
        homePanel.SetActive(false);
        playPanel.SetActive(false);
        playerdata.gamestate = 2;
        SceneManager.LoadScene(1);
    }
}
