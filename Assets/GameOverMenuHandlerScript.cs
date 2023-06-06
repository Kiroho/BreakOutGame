using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenuHandlerScript : MonoBehaviour
{
    public LogicScript logic;
    public TMP_Text scoreText;


    private void Start()
    {
        scoreText.text = "Your Score: " + logic.score.ToString();
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
