using UnityEngine;

public class DobleJumpPowerUp : MonoBehaviour
{
    private Animator _animator;
    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _animator.SetBool("Grabbed", true);
        GameManager.Instance.ActivateDobleJumpPowerUp();
    }
}
