using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnCounterScript : MonoBehaviour {

    GameManager gamemanager;
    Text turnsLeft;
    string originalText;

	// Use this for initialization
	void Start () {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        turnsLeft = GetComponent<Text>();
        originalText = turnsLeft.text;

    }
	
	// Update is called once per frame
	void Update () {
        turnsLeft.text = originalText + gamemanager.GetTurnNumber();
    }
}
