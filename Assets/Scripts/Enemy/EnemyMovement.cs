using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private EntityData EntityData;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private float _direction;

    private LayerMask _solid;
    private LayerMask _spikes;

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();

        _direction = EntityData.InitialDirection;

        _solid = LayerMask.GetMask("Solid");
        _spikes = LayerMask.GetMask("Spikes");
    }

    void Update()
    {
        ProcessMovementDirection();
        if (_rigidBody2D.velocity.x > 0.2f)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_rigidBody2D.velocity.x < -0.2f)
        {
            _spriteRenderer.flipX = true;
        }

        _animator.SetFloat("Velocity", _rigidBody2D.velocity.x);
    }

    private void FixedUpdate()
    {
        _rigidBody2D.AddForce(Vector2.right * _direction * EntityData.Speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    private void ProcessMovementDirection()
    {
        float scaleX = transform.localScale.x * 0.8f;
        float scaleY = transform.localScale.y * 1.2f;
        Vector2 originLeft = _rigidBody2D.position + Vector2.left * scaleX;
        Vector2 originRight = _rigidBody2D.position + Vector2.right * scaleX;
        Debug.DrawRay(originLeft, Vector2.down * scaleY, Color.red);
        Debug.DrawRay(originRight, Vector2.down * scaleY, Color.red);
        RaycastHit2D hitGroundLeft = Physics2D.Raycast(originLeft, Vector2.down, scaleY, _solid);
        if (hitGroundLeft.collider == null)
        {
            ZeroHorizontalVelocity();
            _direction = 1.0f;
        }
        RaycastHit2D hitGroundRight = Physics2D.Raycast(originRight, Vector2.down, scaleY, _solid);
        if (hitGroundRight.collider == null)
        {
            ZeroHorizontalVelocity();
            _direction = -1.0f;
        }

        Vector2 origin = _rigidBody2D.position + Vector2.down * 0.25f;
        Debug.DrawRay(origin, Vector2.left * scaleX, Color.cyan);
        Debug.DrawRay(origin, Vector2.right * scaleX, Color.cyan);
        RaycastHit2D hitWallLeft = Physics2D.Raycast(origin, Vector2.left, scaleX, _solid|_spikes);
        if (hitWallLeft.collider != null)
        {
            ZeroHorizontalVelocity();
            _direction = 1.0f;
        }
        RaycastHit2D hitWallRight = Physics2D.Raycast(origin, Vector2.right, scaleX, _solid|_spikes);
        if (hitWallRight.collider != null)
        {
            ZeroHorizontalVelocity();
            _direction = -1.0f;
        }
    }

    private void ZeroHorizontalVelocity()
    {
        Vector2 newVelocity = _rigidBody2D.velocity;
        newVelocity.x = 0.0f;
        _rigidBody2D.velocity = newVelocity;
    }

}
