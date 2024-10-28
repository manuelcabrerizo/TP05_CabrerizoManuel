using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIOnEnter : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            UIMenuManager.Instance.PlaySelectSound();
        }
        else if(SceneManager.GetActiveScene().name == "GamePlay")
        {
            UIGameplayManager.Instance.PlaySelectSound();
        }
        else if (SceneManager.GetActiveScene().name == "GameOver")
        {
            UIGameOverManager.Instance.PlaySelectSound();
        }
        else if (SceneManager.GetActiveScene().name == "GameWin")
        {
            UIGameWinManager.Instance.PlaySelectSound();
        }
    }
}
