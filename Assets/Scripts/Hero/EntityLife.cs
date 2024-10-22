using System;
using UnityEngine;
using UnityEngine.UI;

public class EntityLife : MonoBehaviour
{ 
    [SerializeField] private EntityData EntityData;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Image lifeImage;

    private int _life;
    public int Life => _life;

    private Rigidbody2D _rigidbody2D;
    private CapsuleCollider2D _collider;
    private ParticleSystem _hitParticleSystem;
    private SpriteRenderer _spriteRenderer;

    private Canvas _canvas;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider = GetComponentInChildren<CapsuleCollider2D>();
        _hitParticleSystem = GetComponentInChildren<ParticleSystem>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _canvas = GetComponentInChildren<Canvas>();
        _life = EntityData.MaxLife;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(CheckCollisionLayer(collision.gameObject, layer))
        {
            DecrementLife();
            Vector2 offset = (Vector2)_rigidbody2D.position - (Vector2)collision.gameObject.transform.position;
            if (offset.SqrMagnitude() > 0)
            {
                offset.Normalize();
                _rigidbody2D.velocity = new Vector2();
                _rigidbody2D.AddForce(offset * EntityData.DamageImpulse, ForceMode2D.Impulse);
            }
            else 
            {
                _rigidbody2D.velocity = new Vector2();
                _rigidbody2D.AddForce(Vector2.up * EntityData.DamageImpulse, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CheckCollisionLayer(collision.gameObject, layer))
        {
            DecrementLife();
            _rigidbody2D.velocity = new Vector2();
            _rigidbody2D.AddForce(Vector2.up * EntityData.DamageImpulse, ForceMode2D.Impulse);
        }
    }

    private bool CheckCollisionLayer(GameObject gameObject, LayerMask layer)
    {
        return ((1 << gameObject.layer) & layer.value) > 0;
    }

    private void DecrementLife()
    {
        _life = Math.Max(_life - 1, 0);
        lifeImage.fillAmount = (float)_life / (float)EntityData.MaxLife;
        _hitParticleSystem.Play();
        if (_life <= 0) {
            _collider.enabled = false;
            _spriteRenderer.enabled = false;
            _canvas.enabled = false;
        }
    }

    public void IncrementLife()
    {
        _life = Math.Min(_life + 1, EntityData.MaxLife);
        lifeImage.fillAmount = (float)_life / (float)EntityData.MaxLife;
    }
}
