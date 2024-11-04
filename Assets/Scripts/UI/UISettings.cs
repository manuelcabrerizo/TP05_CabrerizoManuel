using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    [SerializeField] private Button backButton;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundEffectVolumeSlider;
    [SerializeField] private Slider uiSoundVolumeSlider;

    [SerializeField] private AudioMixer _audioMixer;

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
        float volume = 0;
        // Set Master Volume Slider
        _audioMixer.GetFloat("MasterVolume", out volume);
        masterVolumeSlider.value = Utils.DecibelToLinear(volume);
        // Set Music Volume Slider
        _audioMixer.GetFloat("MusicVolume", out volume);
        musicVolumeSlider.value = Utils.DecibelToLinear(volume);
        // Set Sfx Volume Slider
        _audioMixer.GetFloat("SfxVolume", out volume);
        soundEffectVolumeSlider.value = Utils.DecibelToLinear(volume);
        // Set UI Volume Slider
        _audioMixer.GetFloat("UIVolume", out volume);
        uiSoundVolumeSlider.value = Utils.DecibelToLinear(volume);
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
