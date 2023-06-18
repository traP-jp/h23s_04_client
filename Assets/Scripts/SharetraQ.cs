using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;

public class SharetraQ : MonoBehaviour
{
    [Serializable]
    private sealed class Data{
        public string content;
        public bool embed = false;
    }
    public void OnClick(){
        var url = "https://q.trap.jp/api/v3/channels/f58c72a4-14f0-423c-9259-dbb4a90ca35f/messages";
        var data = new Data();
        data.content="aaa";
        var json = JsonUtility.ToJson(data);
        var postData = Encoding.UTF8.GetBytes(json);
        using var request = new UnityWebRequest(url,UnityWebRequest.kHttpVerbPOST){
            uploadHandler = new UploadHandlerRaw(postData),
            downloadHandler = new DownloadHandlerBuffer()
        };

        request.SetRequestHeader( "Content-Type", "application/json" );

        var operation = request.SendWebRequest();

        operation.completed += _ =>
        {
            Debug.Log( operation.isDone );
            Debug.Log( operation.webRequest.downloadHandler.text );
            Debug.Log( operation.webRequest.isHttpError );
            Debug.Log( operation.webRequest.isNetworkError );
        };

    }
}
