using UnityEngine;
using UnityEngine.UI;

public class Velocity : MonoBehaviour
{
    public Rigidbody character;
    public Text text;

    void Update()
    {
        float speed = character.velocity.magnitude;
        text.text = speed.ToString("F2") + " m/s";
    }
}