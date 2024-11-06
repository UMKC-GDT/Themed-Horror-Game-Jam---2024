using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider sliderVolume;

    public void SetVolume()
    {
        float volume = sliderVolume.value;
        mixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }
}
