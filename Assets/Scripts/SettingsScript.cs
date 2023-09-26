using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SettingsScript : MonoBehaviour
{
    
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown quallityDropdown;

    Resolution[] res;
    [SerializeField]
    private string[] quallitySettingsName;

    private void Start()
    {
        resolutionDropdown.ClearOptions();
        quallityDropdown.ClearOptions();

        List<string> options = new List<string>();
        res = Screen.resolutions;
        int currentResolutionIndex = 0;

        for (int i = 0; i < res.Length; i++) 
        {
            string option = $"{res[i].width} x {res[i].height} + {res[i].refreshRate}";
            options.Add(option);
            if (res[i].width == Screen.currentResolution.width && res[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();

        quallityDropdown.AddOptions(quallitySettingsName.ToList());
        quallityDropdown.RefreshShownValue();

        LoadSettings(currentResolutionIndex);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = res[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", quallityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", System.Convert.ToInt32(Screen.fullScreen));
    }

    public void LoadSettings(int currenResolutionIndex)
    {
        if (PlayerPrefs.HasKey("QualitySettingPreference"))
        {
            quallityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
        }
        else
        {
            quallityDropdown.value = quallitySettingsName.Length - 1;
        }

        if (PlayerPrefs.HasKey("ResolutionPreference"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        }
        else
        {
            resolutionDropdown.value = currenResolutionIndex;
        }

        if (PlayerPrefs.HasKey("FullscreenPreference"))
        {
            Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        }
        else
        {
            Screen.fullScreen = true;
        }
    }

}
