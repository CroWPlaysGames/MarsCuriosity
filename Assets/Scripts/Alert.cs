using System.Collections;
using UnityEngine;

public class Alert : MonoBehaviour
{
    public GameObject warning;
    public bool toggle = true;

    public AudioSource alarm;

    void Update()
    {
        if (toggle)
        {
            StartCoroutine(Blink(0.5f));
        }
    }

    IEnumerator Blink(float time)
    {
        toggle = false;

        yield return new WaitForSeconds(time);

        warning.SetActive(true);
        alarm.Play();

        yield return new WaitForSeconds(time);

        toggle = true;

        warning.SetActive(false);
    }
}
