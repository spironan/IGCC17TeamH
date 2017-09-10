using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharCountUIScript : MonoBehaviour {

    public CharController controller;
    public ICharacter.TYPE type;
    Text counter;
    string originalText;

	// Use this for initialization
	void Start () {
        counter = GetComponent<Text>();
        originalText = counter.text;
    }
	
	// Update is called once per frame
	void Update () {
        counter.text = originalText + controller.GetPossessionCount(type);
    }

}
