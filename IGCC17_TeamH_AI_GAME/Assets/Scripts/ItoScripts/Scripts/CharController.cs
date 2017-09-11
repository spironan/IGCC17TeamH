using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

    List<ICharacter> _characters = new List<ICharacter>();
    ICharacter _currentChara;

    int _possessionArcher;
    int _possessionFighter;
    int _possessionMagician;

    GameObject _archerPrefab;
    GameObject _fighterPrefab;
    GameObject _magicianPrefab;

    IPlayer _owner;

    bool _isPlaying;
    
    // Use this for initialization
    void Start () {
        _possessionArcher = 2;
        _possessionFighter = 2;
        _possessionMagician = 2;
        _currentChara = null;

        _archerPrefab = Resources.Load("Prefab/Archer") as GameObject;
        _fighterPrefab = Resources.Load("Prefab/Fighter") as GameObject;
        _magicianPrefab = Resources.Load("Prefab/Magician") as GameObject;
    }
	
    public void SetOwner(IPlayer owner)
    {
        _owner = owner;
    }

    public ICharacter GetCurrentCharacter()
    {
        return _currentChara;
    }
    public void SetCurrentCharacter(ICharacter chara)
    {
        _currentChara = chara;
    }

    public ICharacter GetOnMouseCharacter()
    {
        foreach(ICharacter chara in _characters)
        {
            if(chara.OnMouse())
            {
                return chara;
            }
        }
        return null;
    }

    public void Generation(int type)
    {
        if (!_isPlaying) return;
        if(_currentChara && _currentChara.X() == -1)
        {
            Destroy(_currentChara.gameObject);
        }
        switch (type)
        {
            case 0: //ICharacter.TYPE.ARCHER:
                if (_possessionArcher <= 0)
                    return;
                _currentChara = Instantiate(_archerPrefab).GetComponent<ICharacter>();
                break;
            case 1: //ICharacter.TYPE.FIGHTER:
                if (_possessionFighter <= 0)
                    return;
                _currentChara = Instantiate(_fighterPrefab).GetComponent<ICharacter>();
                break;
            case 2: //ICharacter.TYPE.MAGICIAN:
                if (_possessionMagician <= 0)
                    return;
                _currentChara = Instantiate(_magicianPrefab).GetComponent<ICharacter>();
                break;
        }

        if (_owner.GetPlayerID() == 1)
            _currentChara.transform.position = new Vector3(3, 0, 0);
        else
        {
            _currentChara.transform.position = new Vector3(-3, 0, 0);
            _currentChara.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }
    }

    public ICharacter SetCharacterOnBoard(ICharacter character)
    {
        switch (character._myType)
        {
            case ICharacter.TYPE.ARCHER:
                _possessionArcher--;
                break;
            case ICharacter.TYPE.FIGHTER:
                _possessionFighter--;
                break;
            case ICharacter.TYPE.MAGICIAN:
                _possessionMagician--;
                break;

        }
        character.SetOnBoard(true);
        _characters.Add(character);
        return character;
    }

    public List<ICharacter> GetCharacters()
    {
        return _characters;
    }

    public int GetPossessionCount(ICharacter.TYPE type)
    {
        switch (type)
        {
            case ICharacter.TYPE.ARCHER:
                return _possessionArcher;
            case ICharacter.TYPE.FIGHTER:
                return _possessionFighter;
            case ICharacter.TYPE.MAGICIAN:
                return _possessionMagician;
            default:
                return -1;
        }
    }

    public int GetGreenCount()
    {
        int greenCount = 0;
        foreach (ICharacter character in _characters)
        {
            if (character.GetMyState() == ICharacter.STATE.GREEN)
                ++greenCount;
        }
        return greenCount;
    }

    public bool IsPlaying()
    {
        return _isPlaying;
    }
    public void IsPlaying(bool nowPlay)
    {
        _isPlaying = nowPlay;
    }
}
