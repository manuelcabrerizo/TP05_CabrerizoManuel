using System.Collections;
using UnityEngine;

public class DragonPowerUpSpawner : MonoBehaviour
{
    static public DragonPowerUpSpawner Instance; 

    private IEnumerator _spawner;
    private bool _pauseCorutine;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
        StopSpawner();
    }

    public void StartSpawner()
    {
        _pauseCorutine = false;
        if (_spawner != null)
        {
            StopSpawner();
        }
        _spawner = ProcessSpawner();
        StartCoroutine(_spawner);
    }

    public void StopSpawner()
    {
        _pauseCorutine = true;
        if (_spawner != null)
        {
            StopCoroutine(_spawner);
            _spawner = null;
        }
    }

    IEnumerator ProcessSpawner()
    {
        while (!_pauseCorutine)
        {
            yield return new WaitForSeconds(Random.Range(2, 10));
            SpawnedPowerUp powerUp = PowerUpSpawner.Instance.SpawnPowerUp(10);
            powerUp.Obj.transform.position = transform.position;
            powerUp.Sprite.enabled = true;
            powerUp.Collider.enabled = true;
            float direction = Random.Range(0, 2) == 0 ? 1 : -1;
            Vector2 impulse = Vector2.right * direction + Vector2.up;
            impulse.Normalize();
            powerUp.Body.AddForce(impulse * 15, ForceMode2D.Impulse);
        }
    }

}
