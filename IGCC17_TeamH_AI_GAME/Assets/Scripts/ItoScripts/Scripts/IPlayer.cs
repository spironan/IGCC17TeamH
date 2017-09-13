using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IPlayer : MonoBehaviour {

    protected CharController _charaController;
    int _playerID;

    Button _archerButton;
    Button _fighterButton;
    Button _magicianButton;
    
    public void Initialize(int id)
    {
        _charaController = GetComponent<CharController>();
        _playerID = id;
        _charaController.SetOwner(this);
        if (id == 1)
        {
            _archerButton = GameObject.Find("Archer").GetComponent<Button>();
            _fighterButton = GameObject.Find("Fighter").GetComponent<Button>();
            _magicianButton = GameObject.Find("Magician").GetComponent<Button>();
        }
        else if (id == 2)
        {
            _archerButton = GameObject.Find("Archer2").GetComponent<Button>();
            _fighterButton = GameObject.Find("Fighter2").GetComponent<Button>();
            _magicianButton = GameObject.Find("Magician2").GetComponent<Button>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (_charaController.IsPlaying())
        {
            _archerButton.interactable = _fighterButton.interactable = _magicianButton.interactable = true;
            Debug.Log("Is This Called?");
        }
        else
        {
            _archerButton.interactable = _fighterButton.interactable = _magicianButton.interactable = false;
            Debug.Log("Is This Called 2 ?");
        }
    }

    public virtual bool SelectCharacter(BoardController boardCon)
    {
        return true;
    }
    
    public virtual bool Action()
    {
        return true;
    }

    public virtual bool Battle()
    {
        return true;
    }

    public virtual void EndProcess()
    {

    }

    public int GetPlayerID()
    {
        return _playerID;
    }

    public CharController GetCharController()
    {
        return _charaController;
    }
}
