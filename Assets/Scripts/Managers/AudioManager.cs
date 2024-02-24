using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField]
    private int sfxPoolSize = 10;
    [SerializeField]
    private AudioSource audioSourcePrefab;

    private List<AudioSource> sfxPool = new List<AudioSource>();

    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        musicSource = gameObject.AddComponent<AudioSource>();

        for (int i = 0; i < sfxPoolSize; i++)
        {
            AudioSource newSource = Instantiate(audioSourcePrefab, transform);
            sfxPool.Add(newSource);
        }
    }

    public void PlayMusic(AudioClip musicClip, float volume = 1f, bool loop = true)
    {
        musicSource.clip = musicClip;
        musicSource.volume = volume;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void PlaySFX(Vector3 position, AudioClip sfxClip, float volume = 1f)
    {
        AudioSource source = sfxPool.Find(s => !s.isPlaying);
        if (source != null)
        {
            source.volume = volume;
            source.PlayOneShot(sfxClip);
            source.transform.position = position;
        }
        else
        {
            Debug.LogWarning("No available AudioSource in the pool.");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void StopSFX()
    {
        foreach (AudioSource source in sfxPool)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        foreach (AudioSource source in sfxPool)
        {
            source.volume = volume;
        }
    }
}

