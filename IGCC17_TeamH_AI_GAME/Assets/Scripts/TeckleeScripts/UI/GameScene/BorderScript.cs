using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorderScript : MonoBehaviour {

    public Sprite blueBorder;
    public Sprite redBorder;
    Image border;
    GameManager gameManager;

	// Use this for initialization
	void Start () {
        border = GetComponent<Image>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameManager.GetTurnNumber() % 2 == 0)
        {
            border.sprite = blueBorder;
        }
        else
        {
            border.sprite = redBorder;
        }

    }
}
