using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnedPowerUp
{
    public GameObject Obj;
    public SpriteRenderer Sprite;
    public CircleCollider2D Collider;
    public int Index;
    public float timer;
}

public class PowerUpSpawner : MonoBehaviour
{
    public static PowerUpSpawner Instance;

    [SerializeField] private List<GameObject> powerUpPrefabs;

    private IObjectPool<SpawnedPowerUp>[] _pools;
    private List<SpawnedPowerUp> _spawnedPowerUp;
    private List<SpawnedPowerUp> _toRelease;

    // throw an exception if we try to return an existing item,
    // already in the pool
    [SerializeField] private bool collectionCheck = true;
    // extra options to control the pool capacity and maximun size
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _pools = new ObjectPool<SpawnedPowerUp>[powerUpPrefabs.Count];
        _pools[0] = new ObjectPool<SpawnedPowerUp>(
            CreatePoolObject0,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPooledObject,
            collectionCheck,
            defaultCapacity,
            maxSize);
        _pools[1] = new ObjectPool<SpawnedPowerUp>(
            CreatePoolObject1,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPooledObject,
            collectionCheck,
            defaultCapacity,
            maxSize);
        _pools[2] = new ObjectPool<SpawnedPowerUp>(
            CreatePoolObject2,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPooledObject,
            collectionCheck,
            defaultCapacity,
            maxSize);

        _spawnedPowerUp = new List<SpawnedPowerUp>();
        _toRelease = new List<SpawnedPowerUp>();
    }

    private void Update()
    {
        UpdateSpawnedPowerUp();
        RemoveDeadPowerUps();
    }

    public SpawnedPowerUp SpawnPowerUp(float lifeTime)
    {
        int index = Random.Range(0, powerUpPrefabs.Count);
        SpawnedPowerUp powerUp = _pools[index].Get();
        powerUp.timer = lifeTime;
        _spawnedPowerUp.Add(powerUp);
        return powerUp;
    }

    private void UpdateSpawnedPowerUp()
    {
        foreach (SpawnedPowerUp powerUp in _spawnedPowerUp)
        {
            powerUp.timer -= Time.deltaTime;
        }
    }

    private void RemoveDeadPowerUps()
    {
        foreach (SpawnedPowerUp powerUp in _spawnedPowerUp)
        {
            if (powerUp.timer <= 0.0f)
            {
                _toRelease.Add(powerUp);
            }
        }

        foreach (SpawnedPowerUp powerUp in _toRelease)
        {
            _pools[powerUp.Index].Release(powerUp);
            _spawnedPowerUp.Remove(powerUp);
        }
        _toRelease.Clear();
    }

    private SpawnedPowerUp CreatePoolObject0()
    {
        GameObject obj = Instantiate(powerUpPrefabs[0]);
        SpawnedPowerUp powerUp = new SpawnedPowerUp();
        powerUp.Obj = obj;
        powerUp.Sprite = obj.GetComponentInChildren<SpriteRenderer>();
        powerUp.Collider = obj.GetComponent<CircleCollider2D>();

        powerUp.Obj.SetActive(false);
        powerUp.Sprite.enabled = false;
        powerUp.Collider.enabled = false;
        powerUp.Index = 0;

        return powerUp;
    }

    private SpawnedPowerUp CreatePoolObject1()
    {
        GameObject obj = Instantiate(powerUpPrefabs[1]);
        SpawnedPowerUp powerUp = new SpawnedPowerUp();
        powerUp.Obj = obj;
        powerUp.Sprite = obj.GetComponentInChildren<SpriteRenderer>();
        powerUp.Collider = obj.GetComponent<CircleCollider2D>();

        powerUp.Obj.SetActive(false);
        powerUp.Sprite.enabled = false;
        powerUp.Collider.enabled = false;
        powerUp.Index = 1;

        return powerUp;
    }

    private SpawnedPowerUp CreatePoolObject2()
    {
        GameObject obj = Instantiate(powerUpPrefabs[2]);
        SpawnedPowerUp powerUp = new SpawnedPowerUp();
        powerUp.Obj = obj;
        powerUp.Sprite = obj.GetComponentInChildren<SpriteRenderer>();
        powerUp.Collider = obj.GetComponent<CircleCollider2D>();

        powerUp.Obj.SetActive(false);
        powerUp.Sprite.enabled = false;
        powerUp.Collider.enabled = false;
        powerUp.Index = 2;

        return powerUp;
    }

    private void OnReleaseToPool(SpawnedPowerUp pooledObject)
    {
        pooledObject.Obj.SetActive(false);
        pooledObject.Sprite.enabled = false;
        pooledObject.Collider.enabled = false;
    }

    private void OnGetFromPool(SpawnedPowerUp pooledObject)
    {
        pooledObject.Obj.SetActive(true);
    }

    private void OnDestroyPooledObject(SpawnedPowerUp pooledObject)
    {
        Destroy(pooledObject.Obj);
    }

    public List<SpawnedPowerUp> GetSpawnedPowerUps()
    {
        return _spawnedPowerUp;
    }

}


