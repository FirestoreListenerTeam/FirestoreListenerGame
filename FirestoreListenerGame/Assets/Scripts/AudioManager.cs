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

    public AudioSource backgroundTerror;

    void Start()
    {
        // Source 1 music
        // TODO: set music

        // Source 2 music
        audioSource2.clip = musicBox;
    }

    public void PlayDrumRoll()
    {
        backgroundTerror.PlayOneShot(drumRoll);
    }

    public void PlaySmokePoof()
    {
        backgroundTerror.PlayOneShot(smokePoof);
    }

    public void PlayClank()
    {
        backgroundTerror.PlayOneShot(clank);
    }

    public void PlayExplosion()
    {
        backgroundTerror.PlayOneShot(explosion);
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