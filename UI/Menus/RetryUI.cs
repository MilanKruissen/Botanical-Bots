using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryUI : MonoBehaviour
{
    public void Retry()
    {
        PlayerManager player = FindObjectOfType<PlayerManager>();
        player.Dead();
        this.gameObject.SetActive(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
