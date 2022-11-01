using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] AudioMixer effectsMixer;
    Slider soundSlider;
    float effectsVol;
    void Start()
    {
        soundSlider = GetComponent<Slider>();
        //soundMixer.GetFloat("soundolume", out soundVol);
        effectsVol = Mathf.Max (PlayerPrefs.GetFloat("EffectsVolume"), 0.005f);
        soundSlider.value = effectsVol;
        effectsMixer.SetFloat("EffectsVolumeParam", Mathf.Log10 (effectsVol) *20);
    }

    public void OnValueChange(float sliderValue)
    {
        effectsVol = sliderValue;
        effectsMixer.SetFloat("EffectsVolumeParam", Mathf.Log10 (effectsVol) *20);
        PlayerPrefs.SetFloat("EffectsVolume", effectsVol);
    }
}
