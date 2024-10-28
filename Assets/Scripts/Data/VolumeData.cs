using UnityEngine;

[CreateAssetMenu(fileName = "VolumeData", menuName = "Volume/Data", order = 1)]

public class VolumeData : ScriptableObject
{
    [Header("Master Volume")]
    public float MasterVolume = 0.5f;
    [Header("Music Volume")]
    public float MusicVolume = 1.0f;
    [Header("Sfx Volume")]
    public float SfxVolume = 1.0f;
    [Header("Ui Volume")]
    public float UIVolume = 1.0f;
}
