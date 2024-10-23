using System.Collections;
using UnityEngine;

public class DragonJumpState : MonoBehaviour, IState
{
    private DragonController _dragonController;
    private EntityLife _dragonLife;
    private Rigidbody2D _rigidbody2D;
    private Rigidbody2D _heroRigidBody2D;

    private float _timePerUpdate;
    private IEnumerator _dragonAi;
    private bool _pauseCorutine;

   
    private void Awake()
    {
        _dragonController = GetComponent<DragonController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _dragonLife = GetComponent<EntityLife>();
        _timePerUpdate = 4.0f;
    }

    private void Start()
    {
        _heroRigidBody2D = GameManager.Instance.Hero.GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {
        StopDragonJumpAI();
    }

    public void Enter()
    {
        StartDragonJumpAI();
        DragonPowerUpSpawner.Instance.StartSpawner();
    }

    public void Exit()
    {
        DragonPowerUpSpawner.Instance.StopSpawner();
        StopDragonJumpAI();
    }

    public void Process(float dt)
    {
        float attackRadio = _dragonController.AttackRatio;
        Vector2 dist = _heroRigidBody2D.position - _dragonController.StartPosition;
        if (dist.SqrMagnitude() > (attackRadio * attackRadio))
        {
            // If the player is out of range return to idle state
            _dragonController.ChangeToIdleState();
        }

        if (_dragonLife.Life <= 50) {
            _dragonController.ChangeToBulletHellState();
        }
    }

    public void FixProcess(float dt)
    {
    }


    public void StartDragonJumpAI()
    {
        _pauseCorutine = false;
        if (_dragonAi != null)
        {
            StopCoroutine(_dragonAi);
        }

        _dragonAi = ProcessDragonJumpAI();
        StartCoroutine(_dragonAi);
    }

    public void StopDragonJumpAI()
    {
        _pauseCorutine = true;
        if (_dragonAi != null)
        {
            StopCoroutine(_dragonAi);
            _dragonAi = null;
        }
    }

    IEnumerator ProcessDragonJumpAI()
    {
        while (!_pauseCorutine)
        {
            yield return new WaitForSeconds(_timePerUpdate);

            Vector2 toHero = _heroRigidBody2D.position - _rigidbody2D.position;
            if (toHero.SqrMagnitude() > 0)
            {
                toHero.Normalize();
                Vector2 impulse = (toHero * 1000.0f) + (Vector2.up * 1500.0f);
                _rigidbody2D.AddForce(impulse, ForceMode2D.Impulse);
            }

        }
    }
}
