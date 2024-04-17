using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public Button menuButton;
    public Button quitButton;
    public Level StartLevel;

    void Start()
    {
        // Assign functionality to menu
        menuButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        // SceneManager.LoadScene(Tutorial);
        SceneManager.LoadScene(0);
    }

}