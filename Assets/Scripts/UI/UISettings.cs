using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    [SerializeField] private Button backButton;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundEffectVolumeSlider;
    [SerializeField] private Slider uiSoundVolumeSlider;

    void Awake()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeSliderValueChange);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderValueChange);
        soundEffectVolumeSlider.onValueChanged.AddListener(OnSoundEffectVolumeSliderValueChange);
        uiSoundVolumeSlider.onValueChanged.AddListener(OnUISoundVolumeSliderValueChange);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
        masterVolumeSlider.onValueChanged.RemoveAllListeners();
        musicVolumeSlider.onValueChanged.RemoveAllListeners();
        soundEffectVolumeSlider.onValueChanged.RemoveAllListeners();
        uiSoundVolumeSlider.onValueChanged.RemoveAllListeners();
    }

    private void OnBackButtonClicked()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            UIMenuManager.Instance.PlayClickSound();
            UIMenuManager.Instance.CloseSettingsMenu();
            UIMenuManager.Instance.OpenMainMenu();
        }
        else
        {
            UIGameplayManager.Instance.PlayClickSound();
            UIGameplayManager.Instance.CloseSettingsMenu();
            UIGameplayManager.Instance.OpenPauseMenu();
        } 
    }

    private void OnMasterVolumeSliderValueChange(float value)
    {
        AudioManager.Instance.SetMasterVolume(value);
    }

    private void OnMusicVolumeSliderValueChange(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }

    private void OnSoundEffectVolumeSliderValueChange(float value)
    {
        AudioManager.Instance.SetSoundEffectVolume(value);
    }

    private void OnUISoundVolumeSliderValueChange(float value)
    {
        AudioManager.Instance.SetUISoundVolume(value);
    }
}
