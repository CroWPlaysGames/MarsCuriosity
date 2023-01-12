using UnityEngine;
using UnityEngine.UI;

public class Icon_Display : MonoBehaviour
{
    public Transform beacon;
    public Vector3 offset;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(beacon.position + offset);

        if (transform.position != pos)
        {
            transform.position = pos;
        }

        if (pos.z < 0)
        {
            gameObject.GetComponent<Image>().enabled = false;
        }

        else
        {
            gameObject.GetComponent<Image>().enabled = true;
        }
    }
}
