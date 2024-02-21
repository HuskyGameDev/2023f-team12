using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class OptionsScreen : MonoBehaviour
{
    public Toggle fullscreenTog, vsyncTog;

    public List<ResItem> resolution = new List<ResItem>();

    private int selectedResolution;

    public TMP_Text resolutionLabel;

    public AudioMixer theMixer;

    public TMP_Text masterLabel, musicLabel, sfxLabel;

    public Slider masterSlider, musicSlider, sfxSlider;

    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else
        {
            vsyncTog.isOn = true;
        }

        bool foundRes = false;
        for (int i = 0; i < resolution.Count; i++)
        {
            if (Screen.width == resolution[i].horizontal && Screen.height == resolution[i].vertical)
            {
                foundRes = true;

                selectedResolution = i;

                UpdateResLabel();
            }
        }

        if (!foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolution.Add(newRes);
            selectedResolution = resolution.Count - 1;

            UpdateResLabel();
        }

        float vol = 0;
        theMixer.GetFloat("MasterVol", out vol);
        masterSlider.value = vol;
        theMixer.GetFloat("MusicVol", out vol);
        musicSlider.value = vol;
        theMixer.GetFloat("SFXVol", out vol);
        sfxSlider.value = vol;

        //masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();
        //musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        // sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
    }



    void Update()
    {

    }

    public void ResLeft()
    {
        selectedResolution--;
        if (selectedResolution < 0)
        {
            selectedResolution = 0;
        }

        UpdateResLabel();
    }

    public void ResRight()
    {
        selectedResolution++;
        if (selectedResolution > resolution.Count - 1)
        {
            selectedResolution = resolution.Count - 1;
        }

        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolution[selectedResolution].horizontal.ToString() + " x " + resolution[selectedResolution].vertical.ToString();
    }

    public void ApplyGraphics()
    {
        //Screen.fullScreen = fullscreenTog.isOn;
        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(resolution[selectedResolution].horizontal, resolution[selectedResolution].vertical, fullscreenTog.isOn);
    }

    public void setMasterVol()
    {
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();

        theMixer.SetFloat("MasterVol", masterSlider.value);

        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
    }
    public void setMusicVol()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

        theMixer.SetFloat("MusicVol", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }
    public void setSFXVol()
    {
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();

        theMixer.SetFloat("SFXVol", sfxSlider.value);

        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;

}
