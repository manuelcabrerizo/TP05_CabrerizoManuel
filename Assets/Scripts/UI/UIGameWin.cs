using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameWin : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;

    void Awake()
    {
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }
    private void Start()
    {
        AudioManager.Instance.PlayMusic();
    }

    private void OnDestroy()
    {
        mainMenuButton.onClick.RemoveAllListeners();
    }

    private void OnMainMenuButtonClicked()
    {
        UIGameWinManager.Instance.PlayClickSound();
        SceneManager.LoadScene("MainMenu");
    }
}
