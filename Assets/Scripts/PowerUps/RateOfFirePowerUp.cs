using UnityEngine;

public class RateOfFirePowerUp : MonoBehaviour
{
    [SerializeField] GameObject _hero;

    private HeroSpell _heroSpell;
    void Awake()
    {
        _heroSpell = _hero.GetComponent<HeroSpell>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _heroSpell.IncrementRateOfFire();

        Destroy(gameObject);
    }

}
