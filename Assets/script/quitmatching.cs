using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quitmatching : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("title");
    }
}
