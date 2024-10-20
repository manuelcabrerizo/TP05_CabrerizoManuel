using UnityEngine;

public class PauseState : IState
{
    public void Enter()
    {
        UIGameplayManager.Instance.OpenPauseMenu();
        AudioManager.Instance.PauseMusic();
        Time.timeScale = 0;   
    }

    public void Exit()
    {
        UIGameplayManager.Instance.ClosePauseMenu();
        UIGameplayManager.Instance.CloseSettingsMenu();
        AudioManager.Instance.PlayMusic();
        Time.timeScale = 1;
    }

    public void Process(float dt)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.PopState();
        }
    }
}
