using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandlerScript : MonoBehaviour
{
    public GameObject pauseBtn;
    public GameObject pauseScreen;

    public void resume()
    {
        Time.timeScale = 1;
        pauseBtn.SetActive(true);
        pauseScreen.SetActive(false);
    }

    public void restart()
    {
        SceneManager.LoadScene("Gamescene");
    }

    public void toMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
