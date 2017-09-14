using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndDisplayScript : MonoBehaviour {

    public GameObject winDisplay, loseDisplay, genericDisplay, background;
    GameManager gamemanager;
    CharController[] controllers;
    bool win, lose;

	// Use this for initialization
	void Start () {
        winDisplay.SetActive(false);
        loseDisplay.SetActive(false);
        genericDisplay.SetActive(false);
        background.SetActive(false);
        GameObject Gamemanager = GameObject.Find("GameManager");
        gamemanager = Gamemanager.GetComponent<GameManager>();
        controllers = Gamemanager.GetComponentsInChildren<CharController>();
        win = lose = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!win && !lose)
        {
            if (gamemanager.GetTurnNumber() == 0)
            {
                lose = true;
            }

            foreach (CharController controller in controllers)
            {
                if (controller.GetGreenCount() != 3)
                    continue;

                if (controller.GetOwner().GetPlayerID() == 1)
                    win = true;
                else
                    lose = true;
            }

            if (win || lose)
            {
                if (win)
                {
                    winDisplay.SetActive(true);
                    SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS,AudioClipManager.GetInstance().GetAudioClip("Win_1"), false, "GenericGameSFX");
                }
                else
                {
                    loseDisplay.SetActive(true);
                    SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("Lose"), false, "GenericGameSFX");
                }
                background.SetActive(true);
                genericDisplay.SetActive(true);
            }
        }

	}
}
