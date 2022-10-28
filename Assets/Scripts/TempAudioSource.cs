using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAudioSource : MonoBehaviour
{
    AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying)
			Destroy(gameObject);
    }
}
