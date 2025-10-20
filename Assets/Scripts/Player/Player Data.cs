using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int gamestate;
    public string IP;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
