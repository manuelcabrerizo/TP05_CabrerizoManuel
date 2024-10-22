using UnityEngine;

public class HeroSpell : MonoBehaviour
{
    [SerializeField] private HeroData HeroData;
    private SpellSpawner _spellSpawner;
    private Rigidbody2D _rigidbody2D;
    private HeroMovement _heroMovement;

    // Start is called before the first frame update
    void Awake()
    {
        _spellSpawner = GetComponent<SpellSpawner>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _heroMovement = GetComponent<HeroMovement>();
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
            spell.Body.AddForce(Vector2.right * 10 * _heroMovement.Direction, ForceMode2D.Impulse);
            spell.Sprite.enabled = true;
            spell.Collider.enabled = true;
        }
    }
}
