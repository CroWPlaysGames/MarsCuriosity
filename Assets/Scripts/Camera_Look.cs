using UnityEngine;

public class Camera_Look : MonoBehaviour
{
    public Transform camera_block;

    public Transform body;

    public GameObject active;

    float xRotation = 0f;

    void Update()
    {       
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            float mouseX = Input.GetAxis("Mouse X") * 100 * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * 100 * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 70f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            camera_block.Rotate(Vector3.up * mouseX);

            active.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            camera_block.localRotation = Quaternion.Euler(0f, 0f, 0f);

            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

            active.SetActive(false);
        }
    }
}
