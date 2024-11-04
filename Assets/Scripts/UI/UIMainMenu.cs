using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void Start()
    {
        AudioManager.Instance.StopMusic();
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();

    }

    private void OnPlayButtonClicked()
    {
        UIMenuManager.Instance.PlayClickSound();
        SceneManager.LoadScene("GamePlay");
    }

    private void OnSettingsButtonClicked()
    {
        UIMenuManager.Instance.PlayClickSound();
        UIMenuManager.Instance.CloseMainMenu();
        UIMenuManager.Instance.OpenSettingsMenu();
    }

    private void OnExitButtonClicked()
    {
        UIMenuManager.Instance.PlayClickSound();
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
