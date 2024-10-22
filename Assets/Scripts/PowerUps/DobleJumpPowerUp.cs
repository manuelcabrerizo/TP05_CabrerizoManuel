using UnityEngine;

public class DobleJumpPowerUp : MonoBehaviour
{
    [SerializeField] GameObject _hero;

    private HeroMovement _heroMovement;

    private Animator _animator;
    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _heroMovement = _hero.GetComponent<HeroMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _animator.SetBool("Grabbed", true);
        _heroMovement.ActivateDobleJump();
    }
}
