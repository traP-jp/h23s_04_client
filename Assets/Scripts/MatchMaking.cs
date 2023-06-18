using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class MatchMaking : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject matching;
    [SerializeField] List<Image> roleImage;
    [SerializeField] Sprite myRole;
    [SerializeField] Sprite yourRole;
    
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.NickName = "Player";
        PhotonNetwork.ConnectUsingSettings();
        
    }
    public override void OnConnectedToMaster() {
        // ランダムなルームに参加する
        PhotonNetwork.JoinRandomRoom();
    }

    // ランダムで参加できるルームが存在しないなら、新規でルームを作成する
    public override void OnJoinRandomFailed(short returnCode, string message) {
        // ルームの参加人数を2人に設定する
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public override void OnCreatedRoom() {
        Debug.Log("ルームの作成に成功しました");
        var hashtable=new ExitGames.Client.Photon.Hashtable();
        hashtable["phase"]=0;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);

    }

    public override void OnJoinedRoom() {
        var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        PhotonNetwork.Instantiate("Avatar", position, Quaternion.identity);
        int role;

        //if (PhotonNetwork.IsMasterClient) {
            //PhotonNetwork.CurrentRoom.SetStartTime(PhotonNetwork.ServerTimestamp);
        //}
        // ルームが満員になったら、以降そのルームへの参加を不許可にする
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers) {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            var hashtable_ = new ExitGames.Client.Photon.Hashtable();
            hashtable_["bgm"] = Random.Range(0,5);
            hashtable_["image"] = Random.Range(0,6);
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable_);
            matching.SetActive(false);
        }
        var hashtable=new ExitGames.Client.Photon.Hashtable();
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1){
            role = 0;
            hashtable["role"]=0;
        } else{
            role = 1;
            hashtable["role"]=1;
        }
        for(int i=0;i<5;i++){
            roleImage[i].sprite = (role == i%2) ? myRole : yourRole;
        }

        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
        GameObject myTurn = GameObject.Find("MyTurn");
        GameObject yourTurn = GameObject.Find("YourTurn");
        if(role == 0){
            myTurn.GetComponent<Canvas>().enabled=true;
            yourTurn.GetComponent<Canvas>().enabled=false;
        } else{
            myTurn.GetComponent<Canvas>().enabled=false;
            yourTurn.GetComponent<Canvas>().enabled=true;
        }
            

    }
    // 他プレイヤーがルームへ参加した時に呼ばれるコールバック
    public override void OnPlayerEnteredRoom(Player newPlayer) {
        Debug.Log($"{newPlayer.NickName}が参加しました");
        matching.SetActive(false);
    }

    // 他プレイヤーがルームから退出した時に呼ばれるコールバック
    public override void OnPlayerLeftRoom(Player otherPlayer) {
        Debug.Log($"{otherPlayer.NickName}が退出しました");
    }
    // Update is called once per frame
    void Update()
    {
        // Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount.ToString());
    }
}
