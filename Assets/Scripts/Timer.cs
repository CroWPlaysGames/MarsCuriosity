using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float time_minutes = 5f;
    public float time_seconds = 0f;

    public Text text;
    public GameObject game_over;
    public GameObject time_lose;

    public GameObject alert;
    public GameObject reason_1;
    public GameObject reason_2;
    public GameObject bounds;
    public GameObject found_alert;

    public AudioSource wind_quiet;
    public AudioSource wind_loud;
    public AudioSource driving;
    public AudioSource ambient;

    void Update()
    {
        if (time_seconds < -0.5f)
        {
            time_minutes--;

            time_seconds += 60f;
        }

        else if (time_seconds < 9.5f)
        {
            time_seconds -= Time.deltaTime;

            text.text = time_minutes.ToString() + ":0" + Mathf.Round(time_seconds).ToString();
        }

        else
        {
            time_seconds -= Time.deltaTime;

            text.text = time_minutes.ToString() + ":" + Mathf.Round(time_seconds).ToString();
        }

        if (time_minutes == 0 && time_seconds < 0)
        {
            wind_quiet.Stop();
            wind_loud.Stop();
            driving.Stop();
            ambient.Stop();

            reason_1.SetActive(false);
            reason_2.SetActive(false);

            game_over.SetActive(true);
            time_lose.SetActive(true);
        }

        else if (time_minutes == 0)
        {      
            if (!found_alert.activeSelf)
            {
                alert.SetActive(true);

                if (!bounds.activeSelf)
                {
                    reason_1.SetActive(true);
                }

                else
                {
                    reason_2.SetActive(true);
                }
            }

            if (reason_2.activeSelf && !bounds.activeSelf)
            {
                reason_2.SetActive(false);
                reason_1.SetActive(true);
            }
        }
    }
}
