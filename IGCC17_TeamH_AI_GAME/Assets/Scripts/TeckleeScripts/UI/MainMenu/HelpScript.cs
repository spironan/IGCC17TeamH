using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScript : MonoBehaviour
{
    bool popUp = false;
    GameObject helpPopUp;

	// Use this for initialization
	void Start ()
    {
        popUp = false;
        helpPopUp = GameObject.FindWithTag("HelpPopUp");
        if (helpPopUp != null)
            helpPopUp.SetActive(popUp);
    }

    public void TogglePopUp()
    {
        popUp = !popUp;
        helpPopUp.SetActive(popUp);
    }

    public void SetPopUp(bool popUpState)
    {
        popUp = popUpState;
        helpPopUp.SetActive(popUp);
    }
    
}
