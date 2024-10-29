using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioClipPitchRandomizer : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    
    [Range(-3, 3)]
    public float lowPitch, highPitch;

    public void PlayClip()
    {
        if(lowPitch > highPitch)
        {
            float temp = lowPitch;
            lowPitch = highPitch;
            highPitch = temp;            
        }

        if(audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if(audioSource.isPlaying)
            return;
        
        audioSource.pitch = Random.Range(lowPitch, highPitch);
        
        audioSource.Play();
    }
}
