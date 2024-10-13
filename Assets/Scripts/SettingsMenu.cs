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
    //Colors
    [SerializeField] Toggle deuteranopiaToggle;
    [SerializeField] Toggle protanopiaToggle;
    [SerializeField] Toggle tritanopiaToggle;
    [SerializeField] Toggle normalVisionToggle;
    // Language
    private bool active = false;

    private int currentPaletteIndex;

    private void Start()
    {
        // Add listeners to the toggles
        normalVisionToggle.isOn = true;
        deuteranopiaToggle.onValueChanged.AddListener(delegate { UpdateColorPalette(); });
        protanopiaToggle.onValueChanged.AddListener(delegate { UpdateColorPalette(); });
        tritanopiaToggle.onValueChanged.AddListener(delegate { UpdateColorPalette(); });
        normalVisionToggle.onValueChanged.AddListener(delegate { UpdateColorPalette(); });

        
    }

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

    private void Update()
    {
        UpdateColorPalette();
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
    
    private void UpdateColorPalette()
    {
        // Check which toggle is selected and set the palette accordingly
        if (deuteranopiaToggle.isOn)
        {
            currentPaletteIndex = VisionUtility.GetColorBlindSafePalette(GetDeuteranopiaPalette(), 0.3f, 0.8f);
            Debug.Log("deuteronopia" +  currentPaletteIndex);
            
        }
        else if (protanopiaToggle.isOn)
        {
            currentPaletteIndex = VisionUtility.GetColorBlindSafePalette(GetProtanopiaPalette(), 0.3f, 0.8f);
            Debug.Log("protanopia" + currentPaletteIndex);

        }
        else if (tritanopiaToggle.isOn)
        {
            currentPaletteIndex = VisionUtility.GetColorBlindSafePalette(GetTritanopiaPalette(), 0.2f, 0.7f);
            Debug.Log("tritanopia" + currentPaletteIndex);
        }
        else
        {
            currentPaletteIndex = VisionUtility.GetColorBlindSafePalette(GetNormalVisionPalette(), 0.2f, 0.9f);
            Debug.Log("normal" + currentPaletteIndex);

        }

        ApplyColorPalette(currentPaletteIndex);
    }

    private Color[] GetDeuteranopiaPalette()
    {
        return new Color[]
        {
            new Color(0f / 255f, 114f / 255f, 178f / 255f),  // Blue
            new Color(230f / 255f, 159f / 255f, 0f / 255f),  // Orange
            new Color(240f / 255f, 228f / 255f, 66f / 255f), // Yellow
        };
    }

    private Color[] GetProtanopiaPalette()
    {
        return new Color[]
        {
            new Color(0f / 255f, 114f / 255f, 178f / 255f),  // Blue
            new Color(213f / 255f, 94f / 255f, 0f / 255f),   // Gold/Orange
            new Color(240f / 255f, 228f / 255f, 66f / 255f), // Yellow
            new Color(204f / 255f, 121f / 255f, 167f / 255f), // Pink (Magenta-like)
            new Color(0f / 255f, 158f / 255f, 115f / 255f),  // Teal
            new Color(86f / 255f, 180f / 255f, 233f / 255f), // Sky Blue
            Color.black                                      // Black for contrast
        };
    }

    private Color[] GetTritanopiaPalette()
    {
        return new Color[]
        {
            new Color(228f / 255f, 26f / 255f, 28f / 255f),  // Red
            new Color(77f / 255f, 175f / 255f, 74f / 255f),  // Green
            new Color(152f / 255f, 78f / 255f, 163f / 255f), // Pink (Magenta-like)
            new Color(255f / 255f, 127f / 255f, 0f / 255f),  // Orange
            new Color(0f / 255f, 150f / 255f, 136f / 255f),  // Teal (leaning toward green)
            new Color(100f / 255f, 60f / 255f, 40f / 255f),  // Dark Brown
            Color.black                                      // Black for contrast
        };
    }

    private Color[] GetNormalVisionPalette()
    {
        return new Color[]
        {
            new Color(213f / 255f, 50f / 255f, 80f / 255f),   // Red
            new Color(0f / 255f, 150f / 255f, 50f / 255f),    // Green
            new Color(0f / 255f, 114f / 255f, 178f / 255f),   // Blue
            new Color(255f / 255f, 223f / 255f, 0f / 255f),   // Yellow
            new Color(255f / 255f, 127f / 255f, 0f / 255f),   // Orange
            new Color(186f / 255f, 123f / 255f, 255f / 255f), // Purple
            new Color(0f / 255f, 175f / 255f, 155f / 255f)    // Teal
        };
    }

    private void ApplyColorPalette(int paletteIndex)
    {
        // Apply the selected color palette to game elements based on the palette index
        // Example: changing background color based on the selected palette
        switch (paletteIndex)
        {
            case 0: // Deuteranopia
                Camera.main.backgroundColor = GetDeuteranopiaPalette()[0]; // Use the first color
                break;
            case 1: // Protanopia
                Camera.main.backgroundColor = GetProtanopiaPalette()[0]; // Use the first color
                break;
            case 2: // Tritanopia
                Camera.main.backgroundColor = GetTritanopiaPalette()[0]; // Use the first color
                break;
            case 3: // Normal Vision
                Camera.main.backgroundColor = GetNormalVisionPalette()[0]; // Use the first color
                break;
            default:
                Debug.LogWarning("Unexpected palette index: " + paletteIndex);
                break;
        }

        // You can also apply other game elements' colors based on your game's needs
        // For example, change material colors, UI colors, etc.
    }
}