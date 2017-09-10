using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinConUIScript : MonoBehaviour {

    public CharController controller;
    Image[] images;
    int greenCounter = 0;
	// Use this for initialization
	void Start () {
        images = GetComponentsInChildren<Image>();
        foreach (Image image in images)
            Debug.Log("Image Size : " + image.name);
        foreach (Image image in images)
            image.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (greenCounter != controller.GetGreenCount())
        {
            greenCounter = controller.GetGreenCount();
            for (int i = 0; i < greenCounter; ++i)
            {
                if(!images[i].gameObject.activeSelf)
                    images[i].gameObject.SetActive(true);
            }
        }
    }
}
