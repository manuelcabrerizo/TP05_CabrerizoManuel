using UnityEngine;

public class RateOfFirePowerUp : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private AudioClipsData AudioClipsData;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Utils.CheckCollisionLayer(collision.gameObject, layer))
        {
            GameManager.Instance.ActivateRofPowerUp();

            foreach (SpawnedPowerUp powerUp in PowerUpSpawner.Instance.GetSpawnedPowerUps())
            {
                if (powerUp.Obj == gameObject)
                {
                    AudioManager.Instance.PlayClip(AudioClipsData.GrabClip, AudioSourceType.SFX);
                    powerUp.Collider.enabled = false;
                    powerUp.Body.bodyType = RigidbodyType2D.Static;
                    powerUp.timer = 0;
                    return;
                }
            }
        }
    }
}
