using UnityEngine;
using UnityEngine.UI;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private HeroData HeroData;
    [SerializeField] private EntityData EntityData;
    [SerializeField] private AudioClipsData AudioClipsData;
    [SerializeField] private Image dobleJumpImage;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private float _movement;
    private float _direction;
    private LayerMask _solid;

    private int _jumpCount;
    private int _maxJumps;
    private float _dobleJumpTimer;

    public float Direction => _direction;

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();

        HeroData.Grounded = HeroData.InitialGrounded;
        _direction = EntityData.InitialDirection;

        _solid = LayerMask.GetMask("Solid");
        _jumpCount = 0;
        _maxJumps = 1;
        _dobleJumpTimer = 0;

    }

    void Update()
    {
        ProcessGrounded();
        ProcessDobleJump();

        _movement = 0.0f;
        if (Input.GetKey(HeroData.MoveRightButton))
        {
            _movement += 1;
            _direction = 1;
            _spriteRenderer.flipX = false;
        }
        if (Input.GetKey(HeroData.MoveLeftButton))
        {
            _movement -= 1;
            _direction = -1;
            _spriteRenderer.flipX = true;
        }

        if (Input.GetKeyDown(HeroData.JumpButton))
        {
            if (HeroData.Grounded)
            {
                _jumpCount = 0;
            }

            if (((_jumpCount == 0) && HeroData.Grounded) ||
                ((_jumpCount > 0) && (_jumpCount < _maxJumps)))
            {
                AudioManager.Instance.PlayClip(AudioClipsData.JumpClip, AudioSourceType.SFX);
                ZeroVerticalVelocity();
                _rigidBody2D.AddForce(Vector2.up * HeroData.JumpImpulse, ForceMode2D.Impulse);
                _jumpCount++;
            }
        }
        // TODO: only update the animator when the values have change
        _animator.SetFloat("Velocity", _rigidBody2D.velocity.x);
        _animator.SetBool("IsGrounded", HeroData.Grounded);
    }

    private void FixedUpdate()
    {
        _rigidBody2D.AddForce(Vector2.right * _movement * EntityData.Speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    private void ProcessGrounded()
    {
        float scaleX = transform.localScale.x * 0.25f;
        float scaleY = transform.localScale.y * 1.2f;
        Vector2 origin0 = _rigidBody2D.position + Vector2.left * scaleX;
        Vector2 origin1 = _rigidBody2D.position + Vector2.right * scaleX;
        Debug.DrawRay(origin0, Vector2.down * scaleY, Color.red);
        Debug.DrawRay(origin1, Vector2.down * scaleY, Color.red);
        RaycastHit2D hitGround0 = Physics2D.Raycast(origin0, Vector2.down, scaleY, _solid);
        RaycastHit2D hitGround1 = Physics2D.Raycast(origin1, Vector2.down, scaleY, _solid);
        if (hitGround0.collider != null || hitGround1.collider != null)
        {
            if (HeroData.Grounded == false) 
            {
                AudioManager.Instance.PlayClip(AudioClipsData.LandClip, AudioSourceType.SFX);
                _jumpCount = 0;
            }
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

    private void ProcessDobleJump()
    {
        if (_dobleJumpTimer <= 0)
        {
            _maxJumps = 1;
        }
        else
        {
            _dobleJumpTimer -= Time.deltaTime;
        }
        dobleJumpImage.fillAmount = _dobleJumpTimer / HeroData.DobleJumpPowerUpDurationInSeconds;
    }

    public void ActivateDobleJump()
    {
        _dobleJumpTimer = HeroData.DobleJumpPowerUpDurationInSeconds;
        _maxJumps = 2;
    }

}
