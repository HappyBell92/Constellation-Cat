using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    [SerializeField] AudioMixer musicMixer;
    Slider musicSlider;
    float musicVol;
    void Start()
    {
        musicSlider = GetComponent<Slider>();
        //musicMixer.GetFloat("Musicolume", out musicVol);
        musicVol = PlayerPrefs.GetFloat("MusicVolume");
        musicSlider.value = musicVol;
        musicMixer.SetFloat("MusicVolumeParam", Mathf.Log10 (musicVol) *20);
    }

    public void OnValueChange(float sliderValue)
    {
        musicVol = sliderValue;
        musicMixer.SetFloat("MusicVolumeParam", Mathf.Log10 (musicVol) *20);
        PlayerPrefs.SetFloat("MusicVolume", musicVol);
    }
}
