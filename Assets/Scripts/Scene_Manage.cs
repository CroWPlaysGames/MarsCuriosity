using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manage : MonoBehaviour
{
    bool paused = false;
    public GameObject pause_menu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            Time.timeScale = 0f;

            paused = true;

            pause_menu.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            Time.timeScale = 1f;

            paused = false;

            pause_menu.SetActive(false);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;

        paused = false;

        pause_menu.SetActive(false);
    }

    public void Main_Menu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Main Menu");
    }
}
