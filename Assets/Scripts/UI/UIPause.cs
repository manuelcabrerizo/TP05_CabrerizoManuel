using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPause : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;

    void Awake()
    {
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    private void OnDestroy()
    {
        resumeButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.RemoveAllListeners();
    }

    private void OnResumeButtonClicked()
    {
        UIGameplayManager.Instance.PlayClickSound();
        GameManager.Instance.PopState();
    }

    private void OnSettingsButtonClicked()
    {
        UIGameplayManager.Instance.PlayClickSound();
        UIGameplayManager.Instance.ClosePauseMenu();
        UIGameplayManager.Instance.OpenSettingsMenu();
    }

    private void OnMainMenuButtonClicked()
    {
        UIGameplayManager.Instance.PlayClickSound();
        SceneManager.LoadScene("MainMenu");
    }
}
