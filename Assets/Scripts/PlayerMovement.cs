using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;

    [SerializeField] private float speed = 30.0f;
    [SerializeField] private float jumpImpulse = 100.0f;

    private float _movement;
    private bool _grounded;

    // Start is called before the first frame update
    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();

        _grounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessGrounded();

        _movement = 0.0f;
        if (Input.GetKey(KeyCode.D))
        {
            _movement += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _movement -= 1;
        }

        if (_grounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            ZeroVerticalVelocity();
            _rigidBody2D.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
        }

    }

    private void FixedUpdate()
    {
        _rigidBody2D.AddForce(Vector2.right * _movement * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    private void ProcessGrounded()
    {
        float scaleX = transform.localScale.x * 0.5f;
        float scaleY = transform.localScale.y * 1.2f;
        Vector2 origin0 = _rigidBody2D.position + Vector2.left * scaleX;
        Vector2 origin1 = _rigidBody2D.position + Vector2.right * scaleX;
        Debug.DrawRay(origin0, Vector2.down * scaleY, Color.red);
        Debug.DrawRay(origin1, Vector2.down * scaleY, Color.red);

        RaycastHit2D hitGround0 = Physics2D.Raycast(origin0, Vector2.down, scaleY, 1 << 9);
        RaycastHit2D hitGround1 = Physics2D.Raycast(origin1, Vector2.down, scaleY, 1 << 9);
        if (hitGround0.collider != null || hitGround1.collider != null)
        {
            _grounded = true;
        }
        else
        {
            _grounded = false;
        } 
    }

    private void ZeroVerticalVelocity()
    {
        Vector2 newVelocity = _rigidBody2D.velocity;
        newVelocity.y = 0.0f;
        _rigidBody2D.velocity = newVelocity;
    }

}
