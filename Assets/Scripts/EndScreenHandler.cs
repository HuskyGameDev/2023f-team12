using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenHandler : MonoBehaviour
{
    public Button quitButton;
    public Button mainMenuButton;

    void Start()
    {
        quitButton.onClick.AddListener(QuitGame);
        mainMenuButton.onClick.AddListener(changeScene);

        // Unlock cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }

    public void changeScene()
    {
        SceneManager.LoadScene(0);
    }
}
