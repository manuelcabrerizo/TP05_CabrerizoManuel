using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class SpawnedSpell
{
    public GameObject Obj;
    public Rigidbody2D Body;
    public SpriteRenderer Sprite;
    public CircleCollider2D Collider;
    public float timer;
}

public class SpellSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spellPrefab;

    private IObjectPool<SpawnedSpell> _spellPool;

    private List<SpawnedSpell> _spawnedSpells;
    private List<SpawnedSpell> _toRelease;


    // throw an exception if we try to return an existing item,
    // already in the pool
    [SerializeField] private bool collectionCheck = true;
    // extra options to control the pool capacity and maximun size
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;


    private void Awake()
    {
        _spellPool = new ObjectPool<SpawnedSpell>(
            CreateSpellObject,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);

        _spawnedSpells = new List<SpawnedSpell>();
        _toRelease = new List<SpawnedSpell>();
    }

    private void Update()
    {
        UpdateSpawnedSpells();
        RemoveDeadSpells();
    }

    public SpawnedSpell SpawnSpell(float lifeTime)
    {
        SpawnedSpell spell = _spellPool.Get();
        spell.timer = lifeTime;
        _spawnedSpells.Add(spell);
        return spell;
    }

    private void UpdateSpawnedSpells()
    {
        foreach (SpawnedSpell spell in _spawnedSpells)
        {
            spell.timer -= Time.deltaTime;
        }
    }

    private void RemoveDeadSpells()
    {
        foreach (SpawnedSpell spell in _spawnedSpells)
        {
            if (spell.timer <= 0.0f)
            {
                _toRelease.Add(spell);
            }
        }

        foreach (SpawnedSpell spell in _toRelease)
        { 
            _spellPool.Release(spell);
            _spawnedSpells.Remove(spell);   
        }
        _toRelease.Clear();
    }

    private SpawnedSpell CreateSpellObject()
    {
        GameObject obj = Instantiate(spellPrefab);
        SpawnedSpell spell = new SpawnedSpell();
        spell.Obj = obj;
        spell.Body = obj.GetComponent<Rigidbody2D>();
        spell.Sprite = obj.GetComponent<SpriteRenderer>();
        spell.Collider = obj.GetComponent<CircleCollider2D>();

        spell.Obj.SetActive(false);
        spell.Sprite.enabled = false;
        spell.Collider.enabled = false;
        spell.Body.position = new Vector2();

        return spell;

    }

    private void OnReleaseToPool(SpawnedSpell pooledObject)
    {
        pooledObject.Obj.SetActive(false);
        pooledObject.Sprite.enabled = false;
        pooledObject.Collider.enabled = false;
        pooledObject.Body.position = new Vector2();
    }

    private void OnGetFromPool(SpawnedSpell pooledObject)
    {
        pooledObject.Obj.SetActive(true);
    }

    private void OnDestroyPooledObject(SpawnedSpell pooledObject)
    {
        Destroy(pooledObject.Obj);
    }
}


