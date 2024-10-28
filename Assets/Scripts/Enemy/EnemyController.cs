using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private EntityLife _entityLife;
    private ParticleSystem _hitParticleSystem;

    void Awake()
    {
        _entityLife = GetComponent<EntityLife>();
        _hitParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (_entityLife.Life <= 0) 
        {
            if (_hitParticleSystem.isStopped)
            {
                SpawnedPowerUp powerUp = PowerUpSpawner.Instance.SpawnPowerUp(10);
                powerUp.Obj.transform.position = transform.position;
                powerUp.Sprite.enabled = true;
                powerUp.Collider.enabled = true;
                if (gameObject.CompareTag("Dragon"))
                {
                    SceneManager.LoadScene("GameWin");
                }
                else
                {
                    Destroy(gameObject);
                }

            }
        }
    }
}
