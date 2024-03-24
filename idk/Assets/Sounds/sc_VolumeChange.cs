using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sc_VolumeChange : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("volume", gameObject.GetComponent<Slider>().value);
        audioSource.volume = PlayerPrefs.GetFloat("volume");
    }

    private void Awake()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume");
    }
}
