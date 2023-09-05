using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingScript : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private bool isMainMenu;

    private readonly string musicVolumeText = "musicVolume";
    private readonly string sfxVolumeText = "sfxVolume";
    private readonly string fullScreenText = "fullScreen";

    void Start()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey(musicVolumeText))
        {
            float musicVolume = PlayerPrefs.GetFloat(musicVolumeText);
            if (!isMainMenu)
            {
                musicSlider.value = musicVolume;
            }
            audioMixer.SetFloat("music", Mathf.Log10(musicVolume) * 20);

        }

        if (PlayerPrefs.HasKey(sfxVolumeText))
        {
            float sfxVolume = PlayerPrefs.GetFloat(sfxVolumeText);
            if (!isMainMenu)
            {
                sfxSlider.value = sfxVolume;
            }
            audioMixer.SetFloat("sfx", Mathf.Log10(sfxVolume) * 20);
        }

        if (PlayerPrefs.HasKey(fullScreenText))
        {
            bool fullScreenValue = bool.Parse(PlayerPrefs.GetString(fullScreenText));
            if (!isMainMenu)
            {
                fullScreenToggle.isOn = fullScreenValue;
            }
            Screen.fullScreen = fullScreenValue;
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(musicVolumeText, volume);
    }

    public void SetSfxVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(sfxVolumeText, volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetString(fullScreenText, isFullScreen.ToString());
    }
}