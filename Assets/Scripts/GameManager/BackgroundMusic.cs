using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    [Range(0, 1)] public float volume = 1f;

    void Start()
    {
        audioSource.clip = clip;
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void Update()
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
