using UnityEngine;

public class script1 : MonoBehaviour
{
    public delegate void MyDelegate(int i);

    public MyDelegate test;

    public void setTest(int j,MyDelegate func)
    {
        test = func;
        test(j);
    }
}
