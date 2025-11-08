using UnityEngine;
using TMPro;
using System;

[Serializable]
public class PlayerData : MonoBehaviour
{
    [SerializeField] TMP_InputField input;

    public int xp = 0;
    public string name;
    public int gamestate;
    public string IP;
    public ulong ID;

    public event Action LoadEvent;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public PlayerData ReturnSelf()
    {
        return this;
    }

    public void Name()
    {
        name = input.text;
    }

    public void Updatedata(int a, string b)
    {
        xp = a;
        name = b;
        LoadEvent.Invoke();
    }
}
