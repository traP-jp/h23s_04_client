using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;


public class SetImage : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Sprite> imageList;

    [SerializeField] Image image;
    // Start is called before the first frame update
    void Start()
    {
        // image.sprite=imageList[0];
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged) {
        foreach (var prop in propertiesThatChanged) {
            if(prop.Key.ToString() == "image"){
                image.sprite=imageList[(int)prop.Value];
            }
        }
    }
}
