using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SavaData : MonoBehaviour
{
    PlayerData playerData;

    private void Awake()
    {
        playerData = GetComponent<PlayerData>();
        SceneManager.sceneLoaded += LoadData;
    }

    private void OnApplicationQuit()
    {
        PlayerData temp = playerData.ReturnSelf();
        playerData = temp;

        string JSON = JsonUtility.ToJson(playerData);

        string dir = Application.persistentDataPath;        string path = Path.Combine(dir, "SaveData.json");

        Directory.CreateDirectory(dir);
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.Write(JSON);
        }
    }

    private void LoadData(Scene scene, LoadSceneMode mode)
    {
        try
        {
            string JSON = string.Empty;

            string dir = Application.persistentDataPath;
            string path = Path.Combine(dir, "SaveData.json");

            Directory.CreateDirectory(dir);

            using (StreamReader reader = new StreamReader(path))
            {
                JSON = reader.ReadToEnd();
            }

            JsonToPlayerBridge data = JsonUtility.FromJson<JsonToPlayerBridge>(JSON);
            playerData.Updatedata(data.xp, data.name);
        }
        catch
        {
            Application.Quit();
        }

        SceneManager.sceneLoaded -= LoadData;
    }


}
