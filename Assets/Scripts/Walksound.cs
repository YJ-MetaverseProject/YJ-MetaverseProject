using Photon.Voice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walksound : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(Object go)
    {
        audioSource.clip = (AudioClip)go;
        audioSource.PlayOneShot(audioSource.clip);
    }
}
