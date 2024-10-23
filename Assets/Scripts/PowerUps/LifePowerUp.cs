using UnityEngine;

public class LifePowerUp : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    private Animator _animator;
    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CheckCollisionLayer(collision.gameObject, layer))
        {
            _animator.SetBool("Grabbed", true);
            GameManager.Instance.ActivateLifePowerUp();
            foreach (SpawnedPowerUp powerUp in PowerUpSpawner.Instance.GetSpawnedPowerUps())
            {
                if (powerUp.Obj == gameObject)
                {
                    powerUp.Collider.enabled = false;
                    powerUp.Body.bodyType = RigidbodyType2D.Static;
                    return;
                }
            }
        }
    }

    private bool CheckCollisionLayer(GameObject gameObject, LayerMask layer)
    {
        return ((1 << gameObject.layer) & layer.value) > 0;
    }
}
