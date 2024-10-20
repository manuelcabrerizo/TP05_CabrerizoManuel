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
        else
        {
            UIGameplayManager.Instance.PlaySelectSound();
        }
    }
}
