using UnityEngine;
using TMPro;

public class PlayerData : MonoBehaviour
{
    [SerializeField] TMP_InputField input;

    public int xp = 0;
    public string name;
    public int gamestate;
    public string IP;
    public ulong ID;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Name()
    {
        name = input.text;
    }

    private void UpdateXP()
    {
        // xp = ;
    }
}
