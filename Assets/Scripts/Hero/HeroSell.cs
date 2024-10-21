using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HeroSell : MonoBehaviour
{
    [SerializeField] private HeroData HeroData;

    private SpellSpawner _spellSpawner;
    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Awake()
    {
        _spellSpawner = GetComponent<SpellSpawner>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(HeroData.FireButton))
        {
            SpawnedSpell spell = _spellSpawner.SpawnSpell(1.0f);
            spell.Obj.transform.position = transform.position;
            spell.Body.position = _rigidbody2D.position;
            spell.Body.velocity = Vector2.Dot(Vector2.right, _rigidbody2D.velocity) * Vector2.right;
            spell.Body.AddForce(Vector2.right * 10 * HeroData.Direction, ForceMode2D.Impulse);
            spell.Sprite.enabled = true;
            spell.Collider.enabled = true;
        }
    }
}
