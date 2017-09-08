using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour {

    enum MAINMENU_OPTIONS
    {
        START_GAME,
        HELP,
        EXIT,
    };

    MAINMENU_OPTIONS currSelect = MAINMENU_OPTIONS.START_GAME;
    Button[] buttons = null;
    PointerEventData pointer;
    EventSystem eventSystem;

    public void ResetLogic()
    {
        Awake();
    }

    // Use this for initialization
    void Awake ()
    {
        currSelect = MAINMENU_OPTIONS.START_GAME;
        eventSystem = GameObject.FindWithTag("EventSystem").GetComponent<EventSystem>();
        pointer = new PointerEventData(EventSystem.current); // pointer event for Execute

        if (buttons == null)
            buttons = GetComponentsInChildren<Button>();

        StartCoroutine(HighlightButton());
    }

    IEnumerator HighlightButton()
    {
        eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        eventSystem.SetSelectedGameObject(buttons[(int)currSelect].gameObject);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown("down"))
        {
            if (currSelect < MAINMENU_OPTIONS.EXIT)
            {
                currSelect++;
                buttons[(int)currSelect].Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown("up"))
        {
            if (currSelect > MAINMENU_OPTIONS.START_GAME)
            {
                currSelect--;
                buttons[(int)currSelect].Select();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        { 
            ExecuteEvents.Execute(buttons[(int)currSelect].gameObject, pointer, ExecuteEvents.submitHandler);
        }

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
