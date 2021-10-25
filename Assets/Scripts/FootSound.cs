using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSound : MonoBehaviour
{
    Vector2 hi;
    [SerializeField]
    private AudioClip[] audioClips;
    private AudioSource audioSource;
    private Transform playerTransform;
    private Animator myAnimator;

    void Start(){
        myAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }
    public void Tak(int hihi){
        if(!myAnimator.GetBool("Run")&&((Mathf.Abs(myAnimator.GetFloat("Blend1"))>Mathf.Abs(myAnimator.GetFloat("Blend2")))?0:1)==hihi)return;
        audioSource.clip = audioClips[Random.Range(0,audioClips.Length-1)];
        audioSource.Play();

    }

}
