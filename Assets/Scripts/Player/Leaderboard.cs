using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{ 
    [SerializeField] GameObject raw;
    
    [SerializeField] int[] positions;

    GameObject current;

    int i = 0;

    public void Add(string name, int pos, float time)
    {
        current = Instantiate(raw, new Vector3(0, positions[i], 0), Quaternion.identity, gameObject.transform);

        RectTransform transform = current.GetComponent<RectTransform>();

        Vector2 ap = transform.anchoredPosition;
        ap = new Vector2(0, positions[i]);
        transform.anchoredPosition = ap;

        i++;

        Debug.Log(time);

        TMP_Text inputName = current.transform.GetChild(0).GetComponent<TMP_Text>();
        inputName.text = name;

        TMP_Text inputPos = current.transform.GetChild(1).GetComponent<TMP_Text>();
        inputPos.text = pos.ToString();

        TMP_Text inputTime = current.transform.GetChild(2).GetComponent<TMP_Text>();
        inputTime.text = time.ToString();
    }
}
