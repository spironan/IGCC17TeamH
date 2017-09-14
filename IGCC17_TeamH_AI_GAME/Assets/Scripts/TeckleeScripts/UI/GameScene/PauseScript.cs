using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    Button pauseBtn;
    GameObject pauseDisplay, rulesDisplay;
    GameManager gameManager;
    CharController playerController;

	// Use this for initialization
	void Start () {
        pauseBtn = GetComponent<Button>();
        pauseDisplay = GameObject.Find("PauseDisplay");
        rulesDisplay = GameObject.Find("RulesDisplay");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.FindWithTag("Player").GetComponent<CharController>();
        SetPauseDisplay(false);
        SetRulesDisplay(false);
    }

    private void Update()
    {
        if (pauseBtn.interactable && !playerController.IsPlaying() && gameManager.GetGameCondition() != GameManager.GAME_CONDITION.SELECT)
            pauseBtn.interactable = false;
        else if (!pauseBtn.interactable && playerController.IsPlaying() && gameManager.GetGameCondition() == GameManager.GAME_CONDITION.SELECT)
            pauseBtn.interactable = true;
    }

    public void SetPauseDisplay(bool active)
    {
        pauseDisplay.SetActive(active);
        gameManager.Pause(active);
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
