using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenuOpener : MonoBehaviour
{

    public static bool Paused = false;
    public GameObject PauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        PauseScreen.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }


    void Stop()
    {
        PauseScreen.SetActive(true);
        Time.timeScale = 0;
        Paused = true;
    }

    public void Play()
    {
        PauseScreen.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }
}
