using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] _musicThemes, _sfx;
    [SerializeField]
    private AudioSource _musicAudioSource, _sfxAudioSource;

    public static AudioManager instance;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        InitializeSoundDataSaveProcess();

        _sfxAudioSource.mute = (PlayerPrefs.GetInt("isSfxMuted") != 0);
    }

    public void PlayMusic(SoundName name)
    {
        Sound musicTheme = Array.Find(_musicThemes, n => n.soundName == name);

        if (musicTheme != null)
        {
            _musicAudioSource.clip = musicTheme.audioClip;
            _musicAudioSource.Play();

            _musicAudioSource.mute = (PlayerPrefs.GetInt("isMusicMuted") != 0);
            _musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
    }

    public void PlaySfx(SoundName name)
    {
        Sound sfx = Array.Find(_sfx, n => n.soundName == name);

        _sfxAudioSource.PlayOneShot(sfx?.audioClip);

        _sfxAudioSource.volume = PlayerPrefs.GetFloat("SfxVolume");
    }

    public void ToggleMusic()
    {
        _musicAudioSource.mute = !_musicAudioSource.mute;

        PlayerPrefs.SetInt("isMusicMuted", _musicAudioSource.mute ? 1 : 0);
    }

    public void ToggleSfx()
    {
        _sfxAudioSource.mute = !_sfxAudioSource.mute;

        PlayerPrefs.SetInt("isSfxMuted", _sfxAudioSource.mute ? 1 : 0);
    }

    public void MusicVolume(float volume)
    {
        _musicAudioSource.volume = volume;

        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SfxVolume(float volume)
    {
       _sfxAudioSource.volume = volume;

        PlayerPrefs.SetFloat("SfxVolume", volume);
    }

    private void InitializeSoundDataSaveProcess()
    {
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
        }

        if (!PlayerPrefs.HasKey("SfxVolume"))
        {
            PlayerPrefs.SetFloat("SfxVolume", 1);
        }
    }
}
