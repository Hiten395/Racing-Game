using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int gamestate;
    public string IP;
    public ulong ID;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
