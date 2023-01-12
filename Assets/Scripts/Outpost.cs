using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Outpost : MonoBehaviour
{
    public GameObject scrollbar;
    public GameObject character;
    public GameObject icon;
    public bool entered = false;
    bool audio_playing = false;

    public AudioSource beep;

    public bool claimed = false;
    public float timer = 0;

    void OnTriggerEnter(Collider coll)
    {
        timer = 0;

        entered = true;

        if (coll.gameObject.tag == "Player" && claimed == false)
        {
            scrollbar.SetActive(true);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        entered = false;

        if (coll.gameObject.tag == "Player" && claimed == false)
        {
            scrollbar.SetActive(false);
            timer = 0;
        }
    }

    IEnumerator Beep(float time)
    {
        audio_playing = true;

        beep.Play();

        yield return new WaitForSeconds(time);

        audio_playing = false;
    }

    void Update()
    {
        if (!audio_playing && !claimed)
        {
            StartCoroutine(Beep(1));
        }

        if (scrollbar.activeSelf == true && entered == true)
        {
            timer = timer + Time.deltaTime;

            scrollbar.GetComponent<Slider>().value = timer;

            if (timer >= 5)
            {
                character.GetComponent<Character_Movement>().score++;

                claimed = true;
                icon.SetActive(false);

                scrollbar.SetActive(false);
            }
        }
    }
}