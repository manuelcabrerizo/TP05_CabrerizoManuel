using UnityEngine;

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
                Destroy(gameObject);
            }
        }
    }
}
