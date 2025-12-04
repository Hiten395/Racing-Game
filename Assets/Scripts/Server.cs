using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Server : MonoBehaviour
{
    int loadMap = 1;

    PlayerData playerData;

    private void Start()
    {
        if (!Application.isBatchMode)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);

            playerData = FindFirstObjectByType<PlayerData>();

            int map = Argument("-a", loadMap);

            playerData.gamestate = 4;

            SceneManager.LoadScene(map);
        }
    }

    int Argument(string a, int fallback)
    {
        var args = Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length - 1; i++)        {            if (args[i].Equals(a, StringComparison.OrdinalIgnoreCase) && int.TryParse(args[i + 1], out var value))            {                return value;            }        }        return fallback;
    }
}
