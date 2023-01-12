using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0f;
    }

    public void Restart_Game()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Main_Menu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Main Menu");
    }
}
