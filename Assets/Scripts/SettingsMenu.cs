using System.Collections;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    //Volume
    [SerializeField] Slider mainVolumeSlider;
    [SerializeField] Slider BGvolumeSlider;
    [SerializeField] Slider SFXvolumeSlider;
    [SerializeField] private AudioMixer mixer;
    
    // Language
    private bool active = false;

   

    public void PauseGame()
    {
        Time.timeScale = 0f;
        settingsPanel.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        settingsPanel.SetActive(false);
    }

    public void AdjustBGMusic()
    {
        mixer.SetFloat("bg_music",BGvolumeSlider.value);
    }

    public void AdjustSFXMusic()
    {
        mixer.SetFloat("sfx", SFXvolumeSlider.value);
    }

    public void AdjustMainMusic()
    {
        mixer.SetFloat("Master", mainVolumeSlider.value);
    }


    public void ChangeLocale(int localeId)
    {
        if (active == true)
        {
            return;
        }
        StartCoroutine(SetLocale(localeId));
    }

    IEnumerator SetLocale(int localeId)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeId];  
        active = false;
    }
    
    
}