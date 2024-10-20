using UnityEngine;

public class UIGameplayManager : MonoBehaviour
{
    public static UIGameplayManager Instance;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsMenuPanel;

    [SerializeField] private AudioClip selectAudioClip;
    [SerializeField] private AudioClip clickAudioClip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ClosePauseMenu();
        CloseSettingsMenu();
    }

    public void OpenPauseMenu()
    {
        pauseMenuPanel.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        pauseMenuPanel.SetActive(false);
    }

    public void OpenSettingsMenu()
    {
        settingsMenuPanel.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        settingsMenuPanel.SetActive(false);
    }

    public void PlaySelectSound()
    {
        AudioManager.Instance.PlayClip(selectAudioClip, AudioSourceType.UI);
    }

    public void PlayClickSound()
    {
        AudioManager.Instance.PlayClip(clickAudioClip, AudioSourceType.UI);
    }
}
