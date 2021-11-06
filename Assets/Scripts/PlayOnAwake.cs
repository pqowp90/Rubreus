using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnAwake : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    private void OnEnable(){
        audioSource.time = 0f;
        audioSource.Play();
        
    }
    private void OnDisable(){
        audioSource.Stop();
    }


}
