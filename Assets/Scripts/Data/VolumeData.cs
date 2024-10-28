using UnityEngine;

[CreateAssetMenu(fileName = "VolumeData", menuName = "Volume/Data", order = 1)]

public class VolumeData : ScriptableObject
{
    [Header("Volume Settings")]
    public float MasterVolume = 0.5f;
    public float MusicVolume = 1.0f;
    public float SfxVolume = 1.0f;
    public float UIVolume = 1.0f;
}
