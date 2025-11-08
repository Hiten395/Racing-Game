using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playPanel;
    [SerializeField] GameObject homePanel;
    [SerializeField] GameObject joinPanel;
    [SerializeField] GameObject mapPanel;
    [SerializeField] TMP_InputField IP;
    [SerializeField] TMP_Text EXP;
    PlayerData playerdata;

    int xp = 0;
    int lvlUp = 100;

    private void Awake()
    {
        playerdata = FindFirstObjectByType<PlayerData>();
        playerdata.LoadEvent += UpdateName;
    }

    private void Start()
    {
        xp = playerdata.xp;
    }

    public void PlayBtn()
    {
        homePanel.SetActive(false);
        playPanel.SetActive(true);
        joinPanel.SetActive(false);
    }

    public void CancelBtn()
    {
        homePanel.SetActive(true);
        playPanel.SetActive(false);
        joinPanel.SetActive(false);
    }

    public void SoloBtn()
    {
        homePanel.SetActive(false);
        playPanel.SetActive(false);
        mapPanel.SetActive(true);
        playerdata.gamestate = 0;
    }

    public void CreateBtn()
    {
        homePanel.SetActive(false);
        playPanel.SetActive(false);
        mapPanel.SetActive(true);
        playerdata.gamestate = 1;    
    }

    public void JoinBtn()
    {
        homePanel.SetActive(false);
        playPanel.SetActive(false);
        joinPanel.SetActive(true);
        playerdata.gamestate = 2;
    }

    public void setIPBtn()
    {
        joinPanel.SetActive(false);
        playerdata.IP = IP.text;
        SceneManager.LoadScene(1);
    }

    public void CreateSceneBtns(int a)
    {
        SceneManager.LoadScene(a);
    }

    public void QuitBtn()
    {
        Application.Quit();
    }

    public void UpdateName()
    {
        EXP.text = playerdata.name + " " + xp.ToString()+"/"+lvlUp.ToString();
    }
}
