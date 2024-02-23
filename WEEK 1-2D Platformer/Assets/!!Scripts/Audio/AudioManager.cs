using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private AudioSource _SFXsource;
    private AudioSource _musicSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        _SFXsource = GetComponent<AudioSource>();
        _musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        ChangeMusicVolume(0);
        ChangeSoundVolume(0);
    }

    public void PlaySound(AudioClip clip)
    {
        _SFXsource.PlayOneShot(clip);
    }

    private void ChangeSourceVolume(float baseVol, string volumeName, float change, AudioSource source)
    {
        float currVol = PlayerPrefs.GetFloat(volumeName, 1);
        currVol += change;

        if (currVol > 1)
            currVol = 0;
        else if (currVol < 0)
            currVol = 1;

        source.volume = currVol * baseVol;

        PlayerPrefs.SetFloat(volumeName, currVol);
    }

    public void ChangeSoundVolume(float change)
    {
        ChangeSourceVolume(1, "SFXVolume", change, _SFXsource);
    }
    public void ChangeMusicVolume(float change)
    {
        ChangeSourceVolume(0.3f, "MusicVolume", change, _musicSource);
    }

}
