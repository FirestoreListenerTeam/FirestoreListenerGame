using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip smokePoof;
    public AudioClip musicBox;
    public AudioClip drumRoll;

    public AudioSource audioSource1;
    public AudioSource audioSource2;

    void Start()
    {
        // Source 1 music
        // TODO: set music

        // Source 2 music
        audioSource2.clip = musicBox;
    }

    public void PlayDrumRoll()
    {
        audioSource1.PlayOneShot(drumRoll);
    }

    public void PlaySmokePoof()
    {
        audioSource1.PlayOneShot(smokePoof);
    }

    public void PlayMusicBox()
    {
        audioSource2.Play();
    }

    public void StopMusicBox()
    {
        audioSource2.Stop();
    }
}
