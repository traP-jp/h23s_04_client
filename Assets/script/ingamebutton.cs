using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ingamebutton : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("ingamescenes");
    }
}
