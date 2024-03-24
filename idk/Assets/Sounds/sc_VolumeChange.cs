using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sc_VolumeChange : MonoBehaviour
{
    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("volume");
    }

    [SerializeField] AudioSource audioSource;
    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("volume", slider.value);
        audioSource.volume = PlayerPrefs.GetFloat("volume");
    }
}
