using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    [SerializeField] private VolumeData VolumeData;

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

    private void Start()
    {
        // Initialize the sliders
        masterVolumeSlider.value = VolumeData.MasterVolume;
        musicVolumeSlider.value = VolumeData.MusicVolume;
        soundEffectVolumeSlider.value = VolumeData.SfxVolume;
        uiSoundVolumeSlider.value = VolumeData.UIVolume;
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
        VolumeData.MasterVolume = value;
        AudioManager.Instance.SetMasterVolume(VolumeData.MasterVolume);
    }

    private void OnMusicVolumeSliderValueChange(float value)
    {
        VolumeData.MusicVolume = value;
        AudioManager.Instance.SetMusicVolume(VolumeData.MusicVolume);
    }

    private void OnSoundEffectVolumeSliderValueChange(float value)
    {
        VolumeData.SfxVolume = value;
        AudioManager.Instance.SetSoundEffectVolume(VolumeData.SfxVolume);
    }

    private void OnUISoundVolumeSliderValueChange(float value)
    {
        VolumeData.UIVolume = value;
        AudioManager.Instance.SetUISoundVolume(VolumeData.UIVolume);
    }
}
