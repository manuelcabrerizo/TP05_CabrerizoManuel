using UnityEngine;

public class SpellManager : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;
    private ParticleSystem _spellParticleSystem;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
        _spellParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _rigidbody2D.velocity = new Vector2();
        _spellParticleSystem.Play();
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
    }
}
