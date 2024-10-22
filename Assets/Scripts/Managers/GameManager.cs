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
    [SerializeField] private GameObject hero;

    private EntityLife _heroLife;
    private HeroMovement _heroMovement;
    private HeroSpell _heroSpell;

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

        _heroLife = hero.GetComponent<EntityLife>();
        _heroMovement = hero.GetComponent<HeroMovement>();
        _heroSpell = hero.GetComponent<HeroSpell>();
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

    public void ActivateLifePowerUp()
    {
        _heroLife.IncrementLife();
    }

    public void ActivateDobleJumpPowerUp()
    {
        _heroMovement.ActivateDobleJump();
    }

    public void ActivateRofPowerUp()
    {
        _heroSpell.IncrementRateOfFire();
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
