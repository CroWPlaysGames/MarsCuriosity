using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public RawImage compass_image;
    public Transform character;
    public Text text;

    void Update()
    {
        compass_image.uvRect = new Rect(character.localEulerAngles.y / 360f, 0, 1, 1);

        Vector3 forward = character.transform.forward;

        forward.y = 0;

        float heading_angle = Quaternion.LookRotation(forward).eulerAngles.y;

        heading_angle = 5 * (Mathf.RoundToInt(heading_angle / 5.0f));

        int display_angle = Mathf.RoundToInt(heading_angle);

        switch (display_angle)
        {
            case 0:
                text.text = "N";
                break;
            case 360:
                text.text = "N";
                break;
            case 45:
                text.text = "NE";
                break;
            case 90:
                text.text = "E";
                break;
            case 135:
                text.text = "SE";
                break;
            case 180:
                text.text = "S";
                break;
            case 225:
                text.text = "SW";
                break;
            case 270:
                text.text = "W";
                break;
            case 315:
                text.text = "NW";
                break;
            default:
                text.text = heading_angle.ToString();
                break;
        }
    }
}
