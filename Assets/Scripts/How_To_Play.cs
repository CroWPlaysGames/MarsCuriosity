using UnityEngine;

public class How_To_Play : MonoBehaviour
{
    public GameObject main_menu;
    public GameObject how_to_play;

    public void Return()
    {
        main_menu.SetActive(true);
        how_to_play.SetActive(false);
    }
}
