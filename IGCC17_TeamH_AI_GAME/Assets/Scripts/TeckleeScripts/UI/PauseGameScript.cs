using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameScript : MonoBehaviour
{
    Text displayText;
    GameObject MainMenuCanvas;

    // Use this for initialization

    private void Awake()
    {
        MainMenuCanvas = GameObject.FindWithTag("MainMenuCanvas");
    }

    public void PauseGame ()
    {
        MainMenuCanvas.SetActive(true);
        if(displayText == null)
            displayText = GameObject.FindWithTag("StartBtn").GetComponent<Text>();
        displayText.text = "Continue Game";
    }
	
}
