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
    private Mute _isMuted;
    
    public AudioSource musicAudioSource, sfxAudioSource;

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
            musicAudioSource.clip = musicTheme.audioClip;
            musicAudioSource.Play();
            musicAudioSource.mute = _isMuted.mute;
        }
    }

    public void PlaySfx(SoundName name)
    {
        Sound sfx = Array.Find(_sfx, n => n.soundName == name);

        if (sfx != null)
        {
            sfxAudioSource.PlayOneShot(sfx.audioClip);
        }
    }

    public void ToggleMusic()
    {
        musicAudioSource.mute = !musicAudioSource.mute;

        _isMuted.mute = musicAudioSource.mute;
    }
}
