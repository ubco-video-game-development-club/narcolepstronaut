using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClipArray;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClipArray[Random.Range(0, audioClipArray.Length)];
        audioSource.Play();
    }

}
