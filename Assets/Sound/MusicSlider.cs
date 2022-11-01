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
        musicVol = Mathf.Max (PlayerPrefs.GetFloat("MusicVolume"), 0.005f);
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
