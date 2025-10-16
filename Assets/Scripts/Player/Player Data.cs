using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int gamestate;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
