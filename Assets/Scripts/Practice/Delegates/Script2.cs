using UnityEngine;

public class Script2 : MonoBehaviour
{
    [SerializeField] script1 script;
    [SerializeField] int i = 5;

    private void Start()
    {
        script.setTest(i, (int k) => { Debug.Log(k); } );
    }
}
