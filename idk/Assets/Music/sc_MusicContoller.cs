using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_MusicContoller : MonoBehaviour
{
    [SerializeField] bool playOnAwake = false;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (playOnAwake)
        {
            Play();
        }
    }

    [SerializeField] AudioClip audioClip;
    public void Play()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.loop = true;
    }
    public void Stop()
    {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = null;
    }
}
