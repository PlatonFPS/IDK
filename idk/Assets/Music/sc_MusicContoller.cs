using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_MusicContoller : MonoBehaviour
{
    [SerializeField] bool playOnAwake = false;
    [SerializeField] AudioSource audioSource;
    private void Awake()
    {
        if (playOnAwake)
        {
            Play();
        }
    }

    [SerializeField] AudioClip audioClip;
    [SerializeField] float pickUpTime;
    [SerializeField] float dropOffTime;
    private bool started = false;
    public void Play()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.loop = true;

        started = true;
        Timer = pickUpTime;
    }
    private bool ending = false;
    public void Stop()
    {
        ending = true;
        Timer = dropOffTime;
    }

    private float Timer = 0f;
    private void Update()
    {
        if (started)
        {
            if(Timer > 0)
            {
                Timer -= Time.deltaTime;
                audioSource.volume = (1 - Timer / pickUpTime) * PlayerPrefs.GetFloat("volume");
            }
            else
            {
                started = false;
                Timer = 0f;
                audioSource.volume = PlayerPrefs.GetFloat("volume");
            }
        }
        if (ending)
        {
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
                audioSource.volume = Timer / dropOffTime * PlayerPrefs.GetFloat("volume");
            }
            else
            {
                ending = false;
                Timer = 0f;
                audioSource.volume = PlayerPrefs.GetFloat("volume");

                audioSource.Stop();
                audioSource.loop = false;
                audioSource.clip = null;
            }
        }
    }
}
