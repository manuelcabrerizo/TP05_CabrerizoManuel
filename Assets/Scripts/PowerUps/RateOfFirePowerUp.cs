using UnityEngine;

public class RateOfFirePowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.ActivateRofPowerUp();

        foreach (SpawnedPowerUp powerUp in PowerUpSpawner.Instance.GetSpawnedPowerUps())
        {
            if (powerUp.Obj == gameObject)
            {
                powerUp.timer = 0;
                return;
            }
        }

        Destroy(gameObject);
    }

}
