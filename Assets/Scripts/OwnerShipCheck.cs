using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class OwnershipSample : MonoBehaviourPunCallbacks
{
    private void Start() {
        // 自身が管理者かどうかを判定する
        if (photonView.IsMine) {
            // 所有者を取得する
            Player owner = photonView.Owner;
            // 所有者のプレイヤー名とIDをコンソールに出力する
            Debug.Log($"{owner.NickName}({photonView.OwnerActorNr})");
        }
        // "RoomObject"プレハブからルームオブジェクトを生成する
        if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.InstantiateRoomObject("RoomObject", Vector3.zero, Quaternion.identity);
        }
    }

}
