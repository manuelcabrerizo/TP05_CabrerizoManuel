using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Entity/Data", order = 1)]

public class EntityData : ScriptableObject
{
    [Header("Stats")]
    public float Speed = 50.0f;
    public int MaxLife = 5;

    [Header("Config")]
    public float InitialDirection = 1.0f;
    public float DamageImpulse = 25.0f;
}
