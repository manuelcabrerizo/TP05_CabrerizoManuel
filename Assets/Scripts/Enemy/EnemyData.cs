using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/Data", order = 1)]

public class EnemyData : ScriptableObject
{
    [Header("Speed")]
    public float Speed = 25.0f;

    [Header("Direction")]
    public float InitialDirection = 1.0f;

    [Header("Stats")]
    public int MaxLife = 3;

    [Header("Sounds")]
    public AudioClip WalkClip;
    public AudioClip JumpClip;
    public AudioClip LandClip;
    public AudioClip FireClip;
    public AudioClip HitClip;
}
