using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour
{
    Abstract baseClass;
    Day day = new Day();
    Night night = new Night();

    private void Start()
    {
        StartCoroutine(DayCycle());
    }

    IEnumerator DayCycle()
    {
        baseClass = day;
        baseClass.UpdateBody();
        yield return new WaitForSeconds(1);
        StartCoroutine(NightCycle());
    }

    IEnumerator NightCycle()
    {
        baseClass = night;
        baseClass.UpdateBody();
        yield return new WaitForSeconds(1);
        StartCoroutine(DayCycle());
    }
}

