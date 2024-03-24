using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_SetVolume : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volume");
    }
}
