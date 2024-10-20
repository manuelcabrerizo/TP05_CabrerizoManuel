using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // States
    private StateMachine stateMachine;
    private PlayState playState;
    private PauseState pauseState;
    public PlayState PlayState => playState;
    public PauseState PauseState => pauseState;

    // Gameplay GameObjects
    // TODO: ...

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

        stateMachine = new StateMachine();
        playState = new PlayState();
        pauseState = new PauseState();
    }

    // Start is called before the first frame update
    private void Start()
    {
        stateMachine.PushState(playState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update(Time.deltaTime);
    }

    public void ResetGameplayObjects()
    {
        // TODO: ???
    }

    public void PushState(IState state)
    {
        stateMachine.PushState(state);
    }

    public void PopState()
    {
        stateMachine.PopState();
    }

    public void ChangeState(IState state)
    {
        stateMachine.ChangeState(state);
    }

    public IState PeekState()
    {
        return stateMachine.PeekState();
    }
}
