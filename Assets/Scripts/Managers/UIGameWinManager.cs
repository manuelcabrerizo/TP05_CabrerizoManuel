using UnityEngine;

public class UIGameWinManager : MonoBehaviour
{
    public static UIGameWinManager Instance;
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
