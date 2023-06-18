using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class screenshot : MonoBehaviour
{
    [SerializeField] List<GameObject> hideOnCapture; 
    [SerializeField] GameObject text;

    void Start(){
        text.SetActive(false);
        
    }
        public void OnClickStartButton()
    {
        
		StartCoroutine(OperationUI ());
    }
    string dt = DateTime.Now.ToString("/yyyy.M.d. H.m.s") + ".png";

    IEnumerator OperationUI() {
        foreach(GameObject gameObject in hideOnCapture){
            gameObject.SetActive(false);
        }
	    ScreenCapture.CaptureScreenshot(Application.dataPath + dt);
        
		yield return new WaitForSeconds (0.1f);
        foreach(GameObject gameObject in hideOnCapture){
            gameObject.SetActive(true);
            
        }
        text.SetActive(true);
		yield return new WaitForSeconds (3f);
        text.SetActive(false);
    }
}
