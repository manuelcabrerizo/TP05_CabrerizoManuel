using UnityEngine;


public class DragonIdleState : MonoBehaviour, IState
{
    private DragonController _dragonController;
    private Rigidbody2D _rigidbody2D;
    private Rigidbody2D _heroRigidBody2D;

    private void Awake()
    {
        _dragonController = GetComponent<DragonController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _heroRigidBody2D = GameManager.Instance.Hero.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float attackRadio = _dragonController.AttackRatio;
        Vector2 origin = _dragonController.StartPosition;
        int segments = 20;
        float increment = (Mathf.PI * 2)/(float)segments;
        float angle = 0;
        for (int i = 0; i < segments; i++)
        {
            Vector2 a = origin + (RotateVector2(Vector2.right, angle) * attackRadio);
            Vector2 b = origin + (RotateVector2(Vector2.right, angle + increment) * attackRadio);
            Debug.DrawLine(a, b, Color.cyan);
            angle += increment;
        }
    }

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void Process(float dt)
    {
        float attackRadio = _dragonController.AttackRatio;
        Vector2 dist = _heroRigidBody2D.position - _dragonController.StartPosition;
        if (dist.SqrMagnitude() <= (attackRadio * attackRadio))
        {
            // If the player is in range change state to jumpState
            _dragonController.ChangeToJumpState();
        }
    }

    public void FixProcess(float dt)
    {
        // if the player is out of range return to the start position
        float attackRadio = _dragonController.AttackRatio;
        Vector2 dist = _heroRigidBody2D.position - _dragonController.StartPosition;
        if (dist.SqrMagnitude() > (attackRadio * attackRadio))
        { 
            Vector2 toStart = _dragonController.StartPosition - _rigidbody2D.position;
            float magnitudeSq = toStart.SqrMagnitude();
            if (magnitudeSq > 0)
            {
                toStart.Normalize();
                _rigidbody2D.AddForce(toStart * 1000 * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
        }
    }

    Vector2 RotateVector2(Vector2 v, float a)
    {
        return new Vector2(
            v.x * Mathf.Cos(a) - v.y * Mathf.Sin(a),
            v.x * Mathf.Sin(a) + v.y * Mathf.Cos(a)
        );
    }
}
