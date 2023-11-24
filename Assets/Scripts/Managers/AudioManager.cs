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

    public void PlayMusic(SoundName name)
    {
        Sound musicTheme = Array.Find(_musicThemes, n => n.soundName == name);
        
        if (musicTheme != null)
        {
            _musicAudioSource.clip = musicTheme.audioClip;
            _musicAudioSource.Play();
        }
    }

    public void PlaySfx(SoundName name)
    {
        Sound sfx = Array.Find(_sfx, n => n.soundName == name);

        if (sfx != null)
        {
            _sfxAudioSource.PlayOneShot(sfx.audioClip);
        }
    }
}
