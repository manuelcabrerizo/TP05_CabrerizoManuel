using UnityEngine;

public class HeroMovement : MonoBehaviour
{

    [SerializeField] private HeroData HeroData;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private float _movement;

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = spriteRenderer.GetComponent<Animator>();

        HeroData.Grounded = HeroData.InitialGrounded;
        HeroData.Direction = HeroData.InitialDirection;
    }

    void Update()
    {
        ProcessGrounded();

        _movement = 0.0f;
        if (Input.GetKey(HeroData.MoveRightButton))
        {
            _movement += 1;
        }
        if (Input.GetKey(HeroData.MoveLeftButton))
        {
            _movement -= 1;
        }

        if (HeroData.Grounded && Input.GetKeyDown(HeroData.JumpButton))
        {
            ZeroVerticalVelocity();
            _rigidBody2D.AddForce(Vector2.up * HeroData.JumpImpulse, ForceMode2D.Impulse);
        }

        if (_rigidBody2D.velocity.x > 0.2f)
        {
            HeroData.Direction = 1;
            spriteRenderer.flipX = false;
        }
        else if (_rigidBody2D.velocity.x < -0.2f)
        {
            HeroData.Direction = -1;
            spriteRenderer.flipX = true;
        }

        _animator.SetFloat("Velocity", _rigidBody2D.velocity.x);
        _animator.SetBool("IsGrounded", HeroData.Grounded);

    }

    private void FixedUpdate()
    {
        _rigidBody2D.AddForce(Vector2.right * _movement * HeroData.Speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    private void ProcessGrounded()
    {
        float scaleX = transform.localScale.x * 0.25f;
        float scaleY = transform.localScale.y * 1.2f;
        Vector2 origin0 = _rigidBody2D.position + Vector2.left * scaleX;
        Vector2 origin1 = _rigidBody2D.position + Vector2.right * scaleX;
        Debug.DrawRay(origin0, Vector2.down * scaleY, Color.red);
        Debug.DrawRay(origin1, Vector2.down * scaleY, Color.red);

        RaycastHit2D hitGround0 = Physics2D.Raycast(origin0, Vector2.down, scaleY, 1 << 9);
        RaycastHit2D hitGround1 = Physics2D.Raycast(origin1, Vector2.down, scaleY, 1 << 9);
        if (hitGround0.collider != null || hitGround1.collider != null)
        {
            HeroData.Grounded = true;
        }
        else
        {
            HeroData.Grounded = false;
        } 
    }

    private void ZeroVerticalVelocity()
    {
        Vector2 newVelocity = _rigidBody2D.velocity;
        newVelocity.y = 0.0f;
        _rigidBody2D.velocity = newVelocity;
    }

}
