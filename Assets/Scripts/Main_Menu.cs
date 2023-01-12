using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public GameObject main_menu;
    public GameObject how_to_play;

    public void Play_Game()
    {
        SceneManager.LoadScene("Mars");
    }

    public void How_To_Play()
    {
        main_menu.SetActive(false);
        how_to_play.SetActive(true);
    }

    public void Quit_Game()
    {
        Application.Quit();
    }
}
