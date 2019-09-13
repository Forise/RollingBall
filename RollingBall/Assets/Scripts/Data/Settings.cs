using UnityEngine;

[System.Serializable, SerializeField]
public struct Settings
{
    public bool isMusicOn;
    public float currentMusicVolume;

    public Settings(bool setDefault)
    {
        isMusicOn = setDefault;
        currentMusicVolume = 0f;
    }
}