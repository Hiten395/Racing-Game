using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playPanel;
    [SerializeField] GameObject homePanel;
    [SerializeField] GameObject joinPanel;
    [SerializeField] TMP_InputField IP;
    [SerializeField] GameObject joinBtn;
    PlayerData playerdata;

    private void Start()
    {
        playerdata = FindFirstObjectByType<PlayerData>();
    }

    public void Play()
    {
        homePanel.SetActive(false);
        playPanel.SetActive(true);
        joinPanel.SetActive(false);
    }

    public void Cancel()
    {
        homePanel.SetActive(true);
        playPanel.SetActive(false);
        joinPanel.SetActive(false);
    }

    public void Solo()
    {
        homePanel.SetActive(false);
        playPanel.SetActive(false);
        playerdata.gamestate = 0;
        SceneManager.LoadScene(1);
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
        joinPanel.SetActive(true);
        playerdata.gamestate = 2;
    }

    public void setIP()
    {
        playerdata.IP = IP.text;
        SceneManager.LoadScene(1);
    }

    public void loadScene()
    {
        SceneManager.LoadScene(1);
    }
}
