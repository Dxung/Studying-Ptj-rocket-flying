using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    AudioSource myAudio;
    private void Start()
    {
        myAudio = this.GetComponent<AudioSource>();
    }

    public void StartSound()
    {
        if (!myAudio.isPlaying)
        {
            myAudio.Play();
        }
    }

    public void StopSound()
    {
        myAudio.Stop();
    }
}
