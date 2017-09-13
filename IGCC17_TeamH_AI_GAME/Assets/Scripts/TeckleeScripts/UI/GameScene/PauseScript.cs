using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    GameObject pauseDisplay, rulesDisplay;

	// Use this for initialization
	void Start () {
        pauseDisplay = GameObject.Find("PauseDisplay");
        rulesDisplay = GameObject.Find("RulesDisplay");
        SetPauseDisplay(false);
        SetRulesDisplay(false);
    }

    public void SetPauseDisplay(bool active)
    {
        pauseDisplay.SetActive(active);
    }

    public void SetRulesDisplay(bool active)
    {
        rulesDisplay.SetActive(active);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenuScene");   
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene("GameScene");
    }
}
