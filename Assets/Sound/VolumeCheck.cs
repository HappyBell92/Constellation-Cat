using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeCheck : MonoBehaviour
{
    [SerializeField] AudioMixer musicMixer;
    [SerializeField] AudioMixer effectsMixer;
    void Start()
    {
        musicMixer.SetFloat("MusicVolumeParam", Mathf.Log10 (PlayerPrefs.GetFloat("MusicVolume")) *20 );
        effectsMixer.SetFloat("EffectsVolumeParam", Mathf.Log10 (PlayerPrefs.GetFloat("EffectsVolume")) *20  );
    }
}
