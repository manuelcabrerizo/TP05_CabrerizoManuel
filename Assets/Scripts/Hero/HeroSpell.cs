using UnityEngine;
using UnityEngine.UI;

public class HeroSpell : MonoBehaviour
{
    [SerializeField] private HeroData HeroData;
    [SerializeField] private Image coolDownImage;
    [SerializeField] private Image rofImage;

    private SpellSpawner _spellSpawner;
    private Rigidbody2D _rigidbody2D;
    private HeroMovement _heroMovement;

    private float _timer;
    private float _rof;

    private float _rofPowerUpTimer;

    private void Awake()
    {
        _spellSpawner = GetComponent<SpellSpawner>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _heroMovement = GetComponent<HeroMovement>();
        _timer = 0;
        _rofPowerUpTimer = 0;
        _rof = HeroData.RateOfFireInSecons;
    }

    private void Update()
    {

        ProcessRofPowerUp();

        _timer -= Time.deltaTime;
        coolDownImage.fillAmount = (_rof - _timer) / _rof;

        if (_rof == HeroData.RateOfFireInSecons)
        {
            if (Input.GetKey(HeroData.FireButton) && (_timer < 0))
            {
                SpawnedSpell spell = _spellSpawner.SpawnSpell(1.0f);
                spell.Obj.transform.position = transform.position;
                spell.Body.position = _rigidbody2D.position;
                spell.Body.velocity = Vector2.Dot(Vector2.right, _rigidbody2D.velocity) * Vector2.right;
                spell.Body.AddForce(Vector2.right * 10 * _heroMovement.Direction, ForceMode2D.Impulse);
                spell.Sprite.enabled = true;
                spell.Collider.enabled = true;
                _timer = _rof;
            }
        }
        else 
        {
            if (_timer < 0)
            {
                SpawnedSpell spell = _spellSpawner.SpawnSpell(1.0f);
                spell.Obj.transform.position = transform.position;
                spell.Body.position = _rigidbody2D.position;
                spell.Body.velocity = Vector2.Dot(Vector2.right, _rigidbody2D.velocity) * Vector2.right;
                spell.Body.AddForce(Vector2.right * 10 * _heroMovement.Direction, ForceMode2D.Impulse);
                spell.Sprite.enabled = true;
                spell.Collider.enabled = true;
                _timer = _rof;
            }
        }
    }

    private void ProcessRofPowerUp()
    {
        if (_rofPowerUpTimer <= 0)
        {
            _rof = HeroData.RateOfFireInSecons;
        }
        else
        {
            _rofPowerUpTimer -= Time.deltaTime;
        }
        rofImage.fillAmount = _rofPowerUpTimer / HeroData.RateOfFirePowerUpDurationInSeconds;
    }

    public void IncrementRateOfFire()
    {
        _rofPowerUpTimer = HeroData.RateOfFirePowerUpDurationInSeconds;
        _rof = HeroData.RateOfFireInSeconsPowerUp;
        _timer = 0;
    }

}
