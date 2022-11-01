using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeCheck : MonoBehaviour
{
    [SerializeField] AudioMixer musicMixer;
    [SerializeField] AudioMixer effectsMixer;

	void Awake() 
	{
		if(PlayerPrefs.GetInt("FirstLaunch") == 0)
		{
			Debug.Log("First launch, volumes reset to max");
			PlayerPrefs.SetFloat("MusicVolume", 1f);
			PlayerPrefs.SetFloat("EffectsVolume", 1f);
			PlayerPrefs.SetInt("FirstLaunch", 1);
		}
	}
    void Start()
    {
        musicMixer.SetFloat("MusicVolumeParam", Mathf.Log10 (PlayerPrefs.GetFloat("MusicVolume")) *20 );
        effectsMixer.SetFloat("EffectsVolumeParam", Mathf.Log10 (PlayerPrefs.GetFloat("EffectsVolume")) *20  );
    }
}
