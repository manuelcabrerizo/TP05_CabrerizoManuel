using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;

    [SerializeField] private float speed = 30.0f;
    [SerializeField] private float jumpImpulse = 100.0f;

    private Vector2 _movement;
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

        _movement = new Vector2();
        if (Input.GetKey(KeyCode.D))
        {
            _movement.x += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _movement.x -= 1;
        }
        if(_movement.SqrMagnitude() > 0)
        {
            _movement.Normalize();
        }

        if (_grounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            _rigidBody2D.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
        }

    }

    private void FixedUpdate()
    {
        _rigidBody2D.AddForce(_movement * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);

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

}
