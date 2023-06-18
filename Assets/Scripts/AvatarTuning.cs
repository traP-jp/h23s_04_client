using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Avatartuning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var localPlayer = PhotonNetwork.LocalPlayer;
        // ローカルプレイヤーの名前を設定する
        PhotonNetwork.NickName = "Player";
        foreach (var player in PhotonNetwork.PlayerList) {
            Debug.Log($"{player.NickName}({player.ActorNumber})");
        }
        // ローカルプレイヤーがマスタークライアントかどうかを判定する
        if (PhotonNetwork.IsMasterClient) {
            Debug.Log("自身がマスタークライアントです");
        }
        // "NetworkedObject"プレパブからネットワークオブジェクトを生成する
PhotonNetwork.Instantiate("NetworkedObject", Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        // ルーム内のプレイヤーオブジェクトの配列（ローカルプレイヤーを含む）を取得する
        var players = PhotonNetwork.PlayerList;
        // ルーム内のプレイヤーオブジェクトの配列（ローカルプレイヤーを含まない）を取得する
        var others = PhotonNetwork.PlayerListOthers;

    }
    
}
