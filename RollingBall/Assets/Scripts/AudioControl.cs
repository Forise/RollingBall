using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoSingleton<AudioControl>
{
    #region Fields
    public UnityEngine.Audio.AudioMixerGroup musicMixer;
    public AudioSource musicSource;
    public bool playMusicOnStart = true;
    #endregion

    private void Start()
    {
        StartCoroutine(Init());
        SetVolumes();
        SettingsControl.Instance.settingsChanged += () =>
        {
            SetVolumes();
        };
        if (playMusicOnStart)
            EventManager.Notify(this, new GameEventArgs("StartMusic"));
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    #region Methods
    private IEnumerator Init()
    {
        yield return new WaitForSeconds(0.15f);
        musicSource.enabled = true;
        yield return null;
    }

    public void PlayMusic(AudioClip clip, bool loop = false)
    {
        if (clip == null /*|| CurrentUserData.MusicOff*/)
            return;

        if (musicSource != null)
        {
            musicSource.outputAudioMixerGroup = musicMixer;
            musicSource.loop = loop;
            musicSource.clip = clip;
            musicSource.time = 0;
            musicSource.Play();
            //Debug.Log(string.Format("Music {0} played", freeSource.clip.name));
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.clip = null;
            musicSource.Stop();
        }
    }

    /// <summary>
    /// Setup misic and sound volumes from settings.
    /// </summary>
    private void SetVolumes()
    {
        var settings = SettingsControl.Instance.settings;
        musicMixer.audioMixer.SetFloat("Volume", settings.isMusicOn ? settings.currentMusicVolume : -80f);
        //floats have to be defined in mixer!!!
    }

    /// <summary>
    /// Setup sound and music volume.
    /// </summary>
    /// <param name="volume">
    /// Min value = -80. Max value = 0.
    /// </param>
    public void SetVolumes(float volume)
    {
        musicMixer.audioMixer.SetFloat("MusicVolume", volume);
    }

    /// <summary>
    /// Mute sound and music (setup volume to MIN).
    /// </summary>
    public void Mute()
    {
        musicMixer.audioMixer.SetFloat("MusicVolume", -80f);
    }

    /// <summary>
    /// Unmute sound and music (setup volume to settings value).
    /// </summary>
    public void Unmute()
    {
        SetVolumes();
    }
    #endregion
}