using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Send : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject yourTurn;
    
    [SerializeField] GameObject myTurn;
    [SerializeField] TextMeshProUGUI phrase1;
    [SerializeField] TextMeshProUGUI phrase2;
    [SerializeField] TextMeshProUGUI phrase3;
    [SerializeField] TextMeshProUGUI phrase4;
    [SerializeField] TextMeshProUGUI phrase5;
    [SerializeField] List<GameObject> finishedUI;
    void Start()
    {
        phrase1.text = "";
        phrase2.text = "";
        phrase3.text = "";
        phrase4.text = "";
        phrase5.text = "";
        foreach(GameObject gameObject in finishedUI){
            gameObject.SetActive(false);
        }
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
            // phrase1.text = text;
        break;
        case 1:
            hashtable["phrase2"]=text;
            // phrase2.text = text;
        break;
        case 2:
            hashtable["phrase3"]=text;
            // phrase3.text = text;
        break;
        case 3:
            hashtable["phrase4"]=text;
            // phrase4.text = text;
        break;
        case 4:
            hashtable["phrase5"]=text;
            // phrase5.text = text;
        break;
        default:
        Debug.Log("aaaa");
        break;
        } 
        hashtable["phase"] = currentPhase+1;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
        Debug.Log("clicked");
        inputField.text="";
        
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

            if(prop.Key.ToString() == "phrase1"){
                Debug.Log(prop.Value);
                phrase1.text=prop.Value.ToString();
            }
            if(prop.Key.ToString() == "phrase2")
                phrase2.text=prop.Value.ToString();
            if(prop.Key.ToString() == "phrase3")
                phrase3.text=prop.Value.ToString();
            if(prop.Key.ToString() == "phrase4")
                phrase4.text=prop.Value.ToString();
            if(prop.Key.ToString() == "phrase5"){
                phrase5.text=prop.Value.ToString();
                //終了
                myTurn.SetActive(false);
                yourTurn.SetActive(false);
                foreach(GameObject gameObject in finishedUI){
                    gameObject.SetActive(true);
                }
            }

            
        } 
    }

    public static int GetCurrentPhase(){
        return (PhotonNetwork.CurrentRoom.CustomProperties["phase"] is int currentPhase ? currentPhase : -1);
    }

    public void BackTitle(){
        SceneManager.LoadScene("titlescene");
    }

}
