using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private EnemyData EnemyData;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private float _direction;

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = spriteRenderer.GetComponent<Animator>();

        _direction = EnemyData.InitialDirection;
    }

    void Update()
    {
        ProcessMovementDirection();
        if (_rigidBody2D.velocity.x > 0.2f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_rigidBody2D.velocity.x < -0.2f)
        {
            spriteRenderer.flipX = true;
        }

        _animator.SetFloat("Velocity", _rigidBody2D.velocity.x);
    }

    private void FixedUpdate()
    {
        _rigidBody2D.AddForce(Vector2.right * _direction * EnemyData.Speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    private void ProcessMovementDirection()
    {
        float scaleX = transform.localScale.x * 0.8f;
        float scaleY = transform.localScale.y * 1.2f;
        Vector2 originLeft = _rigidBody2D.position + Vector2.left * scaleX;
        Vector2 originRight = _rigidBody2D.position + Vector2.right * scaleX;
        Debug.DrawRay(originLeft, Vector2.down * scaleY, Color.red);
        Debug.DrawRay(originRight, Vector2.down * scaleY, Color.red);
        RaycastHit2D hitGroundLeft = Physics2D.Raycast(originLeft, Vector2.down, scaleY, 1 << 9);
        if (hitGroundLeft.collider == null)
        {
            _rigidBody2D.velocity = new Vector2();
            _direction = 1.0f;
        }
        RaycastHit2D hitGroundRight = Physics2D.Raycast(originRight, Vector2.down, scaleY, 1 << 9);
        if (hitGroundRight.collider == null)
        {
            _rigidBody2D.velocity = new Vector2();
            _direction = -1.0f;
        }

        Vector2 origin = _rigidBody2D.position + Vector2.down * 0.25f;
        Debug.DrawRay(origin, Vector2.left * scaleX, Color.cyan);
        Debug.DrawRay(origin, Vector2.right * scaleX, Color.cyan);
        RaycastHit2D hitWallLeft = Physics2D.Raycast(origin, Vector2.left, scaleX, (1 << 9) | (1 << 10));
        if (hitWallLeft.collider != null)
        {
            _rigidBody2D.velocity = new Vector2();
            _direction = 1.0f;
        }
        RaycastHit2D hitWallRight = Physics2D.Raycast(origin, Vector2.right, scaleX, (1 << 9) | (1 << 10));
        if (hitWallRight.collider != null)
        {
            _rigidBody2D.velocity = new Vector2();
            _direction = -1.0f;
        }
    }

}
