using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharCountUIScript : MonoBehaviour {

    public CharController controller;
    public ICharacter.TYPE type;
    Image counter;
    Sprite zero, one, two;

	// Use this for initialization
	void Start () {
        zero = Resources.Load<Sprite>("Assets/GameUI/GamePlayUI/0");
        one = Resources.Load<Sprite>("Assets/GameUI/GamePlayUI/1");
        two = Resources.Load<Sprite>("Assets/GameUI/GamePlayUI/2");
        counter = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        switch (controller.GetPossessionCount(type))
        {
            case 0:
                counter.sprite = zero;
                break;
            case 1:
                counter.sprite = one;
                break;
            case 2:
                counter.sprite = two;
                break;
        }
    }

}
