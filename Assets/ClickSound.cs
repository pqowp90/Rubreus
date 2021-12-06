using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ClickSound : MonoSingleton<ClickSound>
{
    [SerializeField]
    private AudioClip[] audioClips;
    private AudioSource audioSource;
    private void Awake(){
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }
    public void GoSound(int num){
        audioSource.PlayOneShot(audioClips[num]);
    }
}
