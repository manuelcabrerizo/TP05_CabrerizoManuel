using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    [SerializeField] private EntityData EntityData;

    private Rigidbody2D _rigidbody2D;
    private Rigidbody2D _heroRigidBody2D;
    private float _attackRadio;
    private float _timePerUpdate;
    private Vector2 _target;
    
    private IEnumerator _attackHero;
    private bool _pauseCorutine;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _attackRadio = 20.0f;
        _timePerUpdate = 1.0f;
    }


    private void Start()
    {
        _heroRigidBody2D = GameManager.Instance.Hero.GetComponent<Rigidbody2D>();
        _attackHero = AttackHero();
        _pauseCorutine = false;
        StartCoroutine(AttackHero());
        _target = _heroRigidBody2D.position;
    }

    private void OnDestroy()
    {
        //StopCoroutine(_attackHero);
    }

    private void Update()
    {
        Vector2 dist = _target - _rigidbody2D.position;

        float magnitudeSq = dist.SqrMagnitude();
        if ((magnitudeSq > 0) && (magnitudeSq <= (_attackRadio * _attackRadio)))
        {
            dist.Normalize();
            _rigidbody2D.AddForce(dist * EntityData.Speed, ForceMode2D.Force);
        }
    }

    IEnumerator AttackHero()
    {
        while (!_pauseCorutine)
        {
            _target = _heroRigidBody2D.position;
            yield return new WaitForSeconds(_timePerUpdate);
        }
    }
}
