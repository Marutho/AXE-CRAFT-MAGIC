using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer musicVolume;
    public AudioMixer fxsVolume;
    public Dropdown myDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        myDropdown.ClearOptions();

        List<string> options = new List<string>();

        int myRes = 0;

        for (int i = 0;i < resolutions.Length;i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].height==Screen.currentResolution.height &&
                resolutions[i].width==Screen.currentResolution.width)
            {
                myRes = i;
            }
        }

        myDropdown.AddOptions(options);
        myDropdown.value = myRes;
        myDropdown.RefreshShownValue();
    }

    public void MusicVolume (float volume)
    {
        musicVolume.SetFloat("musicVol", volume);
    }

    public void FXSVolume(float volume)
    {
        fxsVolume.SetFloat("fxsVol", volume);
    }

    public void FullScreen(bool s)
    {
        Screen.fullScreen = s;
    }

    public void Quality(int i)
    {
        QualitySettings.SetQualityLevel(i);
    }

    public void Resolution(int r)
    {
        Resolution resolution = resolutions[r];
        Screen.SetResolution(resolution.width,resolution.height, Screen.fullScreen);
    }
}
