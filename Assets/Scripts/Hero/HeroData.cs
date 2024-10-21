using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "Hero/Data", order = 1)]

public class HeroData : ScriptableObject
{
    [Header("Speed")]
    public float Speed = 50.0f;
    
    [Header("Jump")]
    public float JumpImpulse = 50.0f;

    [Header("Direction")]
    public float Direction = 1.0f;
    public float InitialDirection = 1.0f;

    [Header("Grounded")]
    public bool Grounded = false;
    public bool InitialGrounded = false;

    [Header("Stats")]
    public int Life = 3;
    public int MaxLife = 3;
    public int Mana = 100;
    public int MaxMana = 100;

    [Header("Input")]
    public KeyCode MoveLeftButton = KeyCode.A;
    public KeyCode MoveRightButton = KeyCode.D;
    public KeyCode JumpButton = KeyCode.Space;
    public KeyCode FireButton = KeyCode.Return;

    [Header("Sounds")]
    public AudioClip WalkClip;
    public AudioClip JumpClip;
    public AudioClip LandClip;
    public AudioClip FireClip;
    public AudioClip HitClip;
}