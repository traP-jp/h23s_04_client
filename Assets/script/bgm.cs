using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class bgm : MonoBehaviourPunCallbacks
{
    public AudioClip[] audios;
    private AudioSource audioSource;
    private int i;

    void Start()
    {
        // audioSource = this.GetComponent<AudioSource>();
        // if(audios != null)
        // {
        //     i = Random.Range(0,audios.Length);
        //     audioSource.loop = true;
        //     audioSource.PlayOneShot(audios[i]);
        // }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged) {
        foreach (var prop in propertiesThatChanged) {
            if(prop.Key.ToString() == "bgm"){
                audioSource = this.GetComponent<AudioSource>();
                if(audios != null)
                {
                    audioSource.loop = true;
                    audioSource.PlayOneShot(audios[(int)prop.Value]);
                }
            }
        }
    }
}
