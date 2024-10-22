using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "Hero/Data", order = 1)]

public class HeroData : ScriptableObject
{    
    [Header("Jump")]
    public float JumpImpulse = 50.0f;

    [Header("Grounded")]
    public bool Grounded = false;
    public bool InitialGrounded = false;

    [Header("Input")]
    public KeyCode MoveLeftButton = KeyCode.A;
    public KeyCode MoveRightButton = KeyCode.D;
    public KeyCode JumpButton = KeyCode.Space;
    public KeyCode FireButton = KeyCode.Return;
}