using UnityEngine;

public class DobleJumpPowerUp : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private AudioClipsData AudioClipsData;
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
            GameManager.Instance.ActivateDobleJumpPowerUp();
            foreach (SpawnedPowerUp powerUp in PowerUpSpawner.Instance.GetSpawnedPowerUps())
            {
                if (powerUp.Obj == gameObject)
                {
                    AudioManager.Instance.PlayClip(AudioClipsData.GrabClip, AudioSourceType.SFX);
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
