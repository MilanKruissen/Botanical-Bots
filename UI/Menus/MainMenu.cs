using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PlayerManager player;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

    public void PlayButton()
    {
        if (player.saveStarted)
        {
            SceneManager.LoadScene("LevelOne");
        }
        else
        {
            SceneManager.LoadScene("BeginningCutscene");
        }
    }

    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);       
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ResetDataButton()
    {
        player.ResetPlayer();
    }

    public void BackButton()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
