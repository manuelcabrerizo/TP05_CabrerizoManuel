using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuManager : MonoBehaviour
{
    public static UIMenuManager Instance;
    [SerializeField] private GameObject mainMenuPanel;
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

        OpenMainMenu();
        CloseSettingsMenu();
    }

    public void OpenMainMenu()
    {
        mainMenuPanel.SetActive(true);
    }

    public void CloseMainMenu()
    {
        mainMenuPanel.SetActive(false);
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
