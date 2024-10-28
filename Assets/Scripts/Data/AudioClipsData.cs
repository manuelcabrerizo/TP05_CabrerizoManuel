using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipsData", menuName = "AudioClips/Data", order = 1)]

public class AudioClipsData : ScriptableObject
{
    [Header("Music")]
    public AudioClip MainMusicClip;
    public AudioClip BossBattleMusicClip;
    [Header("Sound Effects")]
    public AudioClip JumpClip;
    public AudioClip LandClip;
    public AudioClip FireClip;
    public AudioClip HitClip;
    public AudioClip GrabClip;
    public AudioClip DeadClip;
    public AudioClip GameOverClip;
}
