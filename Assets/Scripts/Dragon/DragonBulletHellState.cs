using System.Collections;
using UnityEngine;

public class DragonBulletHellState : MonoBehaviour, IState
{
    private DragonController _dragonController;
    private Rigidbody2D _rigidbody2D;
    private SpellSpawner _spellSpawner;

    private float _timePerUpdate;
    private IEnumerator _dragonAi;
    private bool _pauseCorutine;

    private float _timer;
    private float _shottingTime;
    private float _movingTime;
    private bool _isShooting;
    private Vector2 _movingDir;

    private void Awake()
    {
        _dragonController = GetComponent<DragonController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spellSpawner = GetComponent<SpellSpawner>();
        _timePerUpdate = 0.05f;

        _isShooting = false;

        _timer = _movingTime;

    }

    public void Enter()
    {
        _movingDir = Random.Range(0, 2) == 0 ? Vector2.right : Vector2.left;
        _shottingTime = Random.Range(2.0f, 5.0f);
        _movingTime = Random.Range(5.0f, 10.0f);
        DragonPowerUpSpawner.Instance.StartSpawner();
    }

    public void Exit()
    {
        DragonPowerUpSpawner.Instance.StopSpawner();
    }

    public void Process(float dt)
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            if (_isShooting)
            {
                // Stop Shooting
                StopDragonBulletAI();
                // Start Moving
                _timer = _movingTime;
                _isShooting = false;
            }
            else
            {
                // Start Shooting
                StartDragonBulletAI();
                _timer = _shottingTime;
                _isShooting = true;
            }
        }
    }

    public void FixProcess(float dt)
    {
        if (!_isShooting)
        {
            Vector2 limitLeft = _dragonController.StartPosition + (Vector2.left * _dragonController.AttackRatio * 0.5f);
            Vector2 limiRight = _dragonController.StartPosition + (Vector2.right * _dragonController.AttackRatio * 0.5f);
            _rigidbody2D.AddForce(_movingDir * 1000 * dt, ForceMode2D.Impulse);
            if (_rigidbody2D.position.x >= limiRight.x)
            {
                _movingDir = Vector2.left;
            }
            if (_rigidbody2D.position.x <= limitLeft.x)
            {
                _movingDir = Vector2.right;
            }
        }
    }


    public void StartDragonBulletAI()
    {
        _pauseCorutine = false;
        if (_dragonAi != null)
        {
            StopCoroutine(_dragonAi);
        }

        _dragonAi = ProcessDragonBulletAI();
        StartCoroutine(_dragonAi);
    }

    public void StopDragonBulletAI()
    {
        _pauseCorutine = true;
        if (_dragonAi != null)
        {
            StopCoroutine(_dragonAi);
            _dragonAi = null;
        }
    }

    IEnumerator ProcessDragonBulletAI()
    {
        while (!_pauseCorutine)
        {
            yield return new WaitForSeconds(_timePerUpdate);


            float angle = Random.Range(0.5f, Mathf.PI - 0.5f);
            Vector2 direction = RotateVector2(Vector2.right, angle);
            SpawnedSpell spell = _spellSpawner.SpawnSpell(1.0f);
            spell.Obj.transform.position = transform.position;
            spell.Body.position = _rigidbody2D.position;
            spell.Body.AddForce(direction * 10, ForceMode2D.Impulse);
            spell.Sprite.enabled = true;
            spell.Collider.enabled = true;
        }
    }

    Vector2 RotateVector2(Vector2 v, float a)
    {
        return new Vector2(
            v.x * Mathf.Cos(a) - v.y * Mathf.Sin(a),
            v.x * Mathf.Sin(a) + v.y * Mathf.Cos(a)
        );
    }
}
