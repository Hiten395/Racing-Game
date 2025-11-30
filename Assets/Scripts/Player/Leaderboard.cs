using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] GameObject Raw;

    [SerializeField] int[] positions;

    RectTransform rectTransform;
    GameObject current;

    int i = 0;

    public void Add()
    {
        current = Instantiate(Raw, new Vector3(0,0,0), Quaternion.identity, transform);
        rectTransform = current.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, positions[i]);
        i++;
    }
}
