using UnityEngine;

public class DragonController : MonoBehaviour
{
    private DragonIdleState _idleState;
    private DragonJumpState _jumpState;
    private DragonBulletHellState _bulletHellState;
    private StateMachine _stateMachine;

    private Vector2 _startPosition;
    [SerializeField] private float attackRadio = 20.0f;

    public Vector2 StartPosition => _startPosition;
    public float AttackRatio => attackRadio;


    private void Awake()
    {
        _stateMachine = new StateMachine();
        _idleState = GetComponent<DragonIdleState>();
        _jumpState = GetComponent<DragonJumpState>();
        _bulletHellState = GetComponent<DragonBulletHellState>();
    }

    private void Start()
    {
        _startPosition = transform.position;
        _stateMachine.PushState(_idleState);
    }

    private void Update()
    {
        _stateMachine.Update(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _stateMachine.FixUpdate(Time.fixedDeltaTime);
    }

    public void ChangeToIdleState()
    {
        _stateMachine.ChangeState(_idleState);
    }

    public void ChangeToJumpState()
    {
        _stateMachine.ChangeState(_jumpState);
    }

    public void ChangeToBulletHellState()
    {
        _stateMachine.ChangeState(_bulletHellState);
    }
}
