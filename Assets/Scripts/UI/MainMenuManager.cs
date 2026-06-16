using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static bool skipMainMenu = false;

    [SerializeField]
    private GameObject mainMenuPanel;

    private void Start()
    {
        if (skipMainMenu)
        {
            skipMainMenu = false;

            mainMenuPanel.SetActive(false);

            Time.timeScale = 1f;
        }
        else
        {
            mainMenuPanel.SetActive(true);

            Time.timeScale = 0f;
        }
    }

    public void PlayGame()
    {
        mainMenuPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();

        Debug.Log("Exit Game");
    }
}