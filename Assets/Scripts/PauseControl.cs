using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour
{
    public Canvas popUpMenu;
    public static bool gameIsPaused;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }
    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            // Pop up the menu here
            popUpMenu.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            // Close the menu here
            popUpMenu.gameObject.SetActive(false);
        }
    }

}
