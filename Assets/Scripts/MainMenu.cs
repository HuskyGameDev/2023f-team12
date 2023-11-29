using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Level StartLevel;

    public Button startButton;
    public Button optsButton;
    public Button quitButton;

    public GameObject OptionsScreen;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        optsButton.onClick.AddListener(OpenOptions);
        quitButton.onClick.AddListener(QuitGame);
    }


    void Update()
    {

    }

    public void StartGame()
    {
        // SceneManager.LoadScene(Tutorial);
        Global.LoadNewLevel(StartLevel);
    }

    public void OpenOptions()
    {
        OptionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        OptionsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}

