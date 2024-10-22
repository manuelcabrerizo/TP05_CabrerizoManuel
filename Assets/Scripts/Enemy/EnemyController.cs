using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EntityLife _entityLife;

    void Awake()
    {
        _entityLife = GetComponent<EntityLife>();
    }

    void Update()
    {
        if (_entityLife.Life <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
