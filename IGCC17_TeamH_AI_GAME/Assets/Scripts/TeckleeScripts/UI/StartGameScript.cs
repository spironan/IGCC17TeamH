using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameScript : MonoBehaviour
{
    Text displayText;
    GameObject GameCanvas;

    void Awake()
    {
        displayText = GetComponentInChildren<Text>();
        displayText.text = "Start Game";
        GameCanvas = GameObject.FindWithTag("GameCanvas");
        GameCanvas.SetActive(false);
    }

    public void StartGame()
    {
        GameCanvas.SetActive(true);
        gameObject.transform.parent.gameObject.SetActive(false);
    }

}
