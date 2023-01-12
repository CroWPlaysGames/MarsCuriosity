using UnityEngine;

public class Wind : MonoBehaviour
{
    public float strength;
    public Vector3 direction;
    public string angle;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, gameObject.GetComponent<BoxCollider>().size);
    }

    void Update()
    {
        float value = strength;

        switch (angle)
        {
            case "N":
                direction = new Vector3(0, 0, value);
                break;
            case "S":
                direction = new Vector3(0, 0, -value);
                break;
            case "E":
                direction = new Vector3(value, 0, 0);
                break;
            case "W":
                direction = new Vector3(-value, 0, 0);
                break;
            case "NW":
                direction = new Vector3(-value, 0, value);
                break;
            case "NE":
                direction = new Vector3(value, 0, value);
                break;
            case "SW":
                direction = new Vector3(-value, 0, -value);
                break;
            case "SE":
                direction = new Vector3(value, 0, -value);
                break;
            default:
                direction = Vector3.zero;
                break;
        }
    }
}
