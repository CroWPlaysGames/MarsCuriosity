using UnityEngine;

public class Fall_Over : MonoBehaviour
{
    public GameObject game_over;
    public GameObject fall_lose;

    public AudioSource wind_quiet;
    public AudioSource wind_loud;
    public AudioSource driving;
    public AudioSource ambient;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            wind_quiet.Stop();
            wind_loud.Stop();
            driving.Stop();
            ambient.Stop();

            game_over.SetActive(true);
            fall_lose.SetActive(true);
        }
    }
}
