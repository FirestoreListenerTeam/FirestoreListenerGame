using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip smokePoof;
    public AudioClip musicBox;
    public AudioClip drumRoll;
    public AudioClip clank;
    public AudioClip explosion;

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

    public void PlayClank()
    {
        audioSource1.PlayOneShot(clank);
    }

    public void PlayExplosion()
    {
        audioSource1.PlayOneShot(explosion);
    }

    public void PlayMusicBox()
    {
        audioSource2.Play();
        Debug.Log("Play music box");
    }

    public void StopMusicBox()
    {
        audioSource2.Pause();
        Debug.Log("Stop music box");
    }
}