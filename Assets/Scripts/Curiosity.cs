using UnityEngine;

public class Curiosity : MonoBehaviour
{
    public GameObject found_alert;
    public GameObject you_win;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            you_win.SetActive(true);
        }
    }
}
