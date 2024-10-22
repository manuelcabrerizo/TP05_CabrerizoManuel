using UnityEngine;

public class LifePowerUp : MonoBehaviour
{
    [SerializeField] GameObject _hero;

    private Animator _animator;
    private EntityLife _heroLife;
    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _heroLife = _hero.GetComponent<EntityLife>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        _animator.SetBool("Grabbed", true);
        _heroLife.IncrementLife();
    }
}
