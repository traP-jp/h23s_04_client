using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgm : MonoBehaviour
{
    public AudioClip[] audios;
    private AudioSource audioSource;
    private int i;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        if(audios != null)
        {
            i = Random.Range(0,audios.Length);
            audioSource.loop = true;
            audioSource.PlayOneShot(audios[i]);
        }
    }
}
