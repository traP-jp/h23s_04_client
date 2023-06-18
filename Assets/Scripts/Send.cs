using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using ExitGames.Client.Photon;
using UnityEngine.UI;


public class Send : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject yourTurn;
    
    [SerializeField] GameObject myTurn;
    void Start()
    {
        //text = input.text;
    }
    
    public void OnClick()
    {
        string text = inputField.text;
        var hashtable=new ExitGames.Client.Photon.Hashtable();
        int currentPhase = GetCurrentPhase();
        Debug.Log(currentPhase);
        //TMP_inputField = CurrentRoomCustomProperty["phrase1"];
        switch(currentPhase)
        {
        case 0:
            hashtable["phrase1"]=text;
        break;
        case 1:
            hashtable["phrase2"]=text;
        break;
        case 2:
            hashtable["phrase3"]=text;
        break;
        case 3:
            hashtable["phrase4"]=text;
        break;
        case 4:
            hashtable["phrase5"]=text;
        break;
        default:
        Debug.Log("aaaa");
        break;
        } 
        hashtable["phase"] = currentPhase+1;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
        Debug.Log("clicked");
        
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged) {
        // 更新されたルームのカスタムプロパティのペアをコンソールに出力する
        foreach (var prop in propertiesThatChanged) {
            myTurn = GameObject.Find("MyTurn");
            yourTurn = GameObject.Find("YourTurn");
            if(prop.Key.ToString() == "phase" && (int)prop.Value != 0){
                if(((int)PhotonNetwork.CurrentRoom.CustomProperties["phase"] + (int)PhotonNetwork.LocalPlayer.CustomProperties["role"])%2==0){
                    // if((int)PhotonNetwork.CurrentRoom.CustomProperties["phase"]%2==0 && (int)PhotonNetwork.LocalPlayer.CustomProperties["role"]==0){
                        myTurn.GetComponent<Canvas>().enabled=true;
                        yourTurn.GetComponent<Canvas>().enabled=false;
                    }else /*if((int)PhotonNetwork.CurrentRoom.CustomProperties["phase"]%2!=0 && (int)PhotonNetwork.LocalPlayer.CustomProperties["role"]==1)*/{
                        myTurn.GetComponent<Canvas>().enabled=false;
                        yourTurn.GetComponent<Canvas>().enabled=true;
                    }
                
            }   
        } 
    }

    public static int GetCurrentPhase(){
        return (PhotonNetwork.CurrentRoom.CustomProperties["phase"] is int currentPhase ? currentPhase : -1);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
