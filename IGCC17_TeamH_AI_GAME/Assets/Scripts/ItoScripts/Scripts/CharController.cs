using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour {

    List<ICharacter> _characters = new List<ICharacter>();
    ICharacter _currentChara;

    int _possessionFighter;
    int _possessionArcher;
    int _possessionMagician;

    GameObject _fighterPrefab;
    GameObject _archerPrefab;
    GameObject _magicianPrefab;

    Sprite _wonFighterSprite;
    Sprite _wonArcherSprite;
    Sprite _wonMagicianSprite;

    Sprite _cutInFighter;
    Sprite _cutInArcher;
    Sprite _cutInMagician;

    GameObject _cutIn;

    IPlayer _owner;

    bool _isPlaying;

    GameObject _summonEffect;
    
    // Use this for initialization
    void Start () {
        _possessionFighter = 2;
        _possessionArcher = 2;
        _possessionMagician = 2;

        _currentChara = null;
        _cutIn = GameObject.Find("CutIn");
        _summonEffect = Resources.Load("Prefab/SummonEffect") as GameObject;
    }

    public IPlayer GetOwner()
    {
        return _owner;
    }
    public void SetOwner(IPlayer owner)
    {
        _owner = owner;
        if (owner.GetPlayerID() == 1)
        {
            _fighterPrefab = Resources.Load("Prefab/Blue_Fighter") as GameObject;
            _archerPrefab = Resources.Load("Prefab/Blue_Archer") as GameObject;
            _magicianPrefab = Resources.Load("Prefab/Blue_Magician") as GameObject;

            _wonFighterSprite = Resources.Load<Sprite>("Assets/GameBlocks/Blue01");
            _wonArcherSprite = Resources.Load<Sprite>("Assets/GameBlocks/Blue03");
            _wonMagicianSprite = Resources.Load<Sprite>("Assets/GameBlocks/Blue02");

            _cutInFighter = Resources.Load<Sprite>("Assets/GameCharacter/slide/Warrior_Blue_slide");
            _cutInArcher = Resources.Load<Sprite>("Assets/GameCharacter/slide/Archer_Blue_slide");
            _cutInMagician = Resources.Load<Sprite>("Assets/GameCharacter/slide/Magic_Blue_slide");

            Debug.Log("Set Prefabs For Player!");
        }
        else if (owner.GetPlayerID() == 2)
        {
            _fighterPrefab = Resources.Load("Prefab/Red_Fighter") as GameObject;
            _archerPrefab = Resources.Load("Prefab/Red_Archer") as GameObject;
            _magicianPrefab = Resources.Load("Prefab/Red_Magician") as GameObject;

            _wonFighterSprite = Resources.Load<Sprite>("Assets/GameBlocks/Red01");
            _wonArcherSprite = Resources.Load<Sprite>("Assets/GameBlocks/Red03");
            _wonMagicianSprite = Resources.Load<Sprite>("Assets/GameBlocks/Red02");

            _cutInFighter = Resources.Load<Sprite>("Assets/GameCharacter/slide/Warrior_Red_slide");
            _cutInArcher = Resources.Load<Sprite>("Assets/GameCharacter/slide/Archer_Red_slide");
            _cutInMagician = Resources.Load<Sprite>("Assets/GameCharacter/slide/Magic_Red_slide");

            Debug.Log("Set Prefabs For AI!");
        }
    }

    public void PlayCutIn(ICharacter character)
    {
        Image image = _cutIn.GetComponent<Image>();
        image.enabled = true;
        switch (character._myType)
        {
            case ICharacter.TYPE.FIGHTER:
                image.sprite = _cutInFighter;
                break;
            case ICharacter.TYPE.ARCHER:
                image.sprite = _cutInArcher;
                break;
            case ICharacter.TYPE.MAGICIAN:
                image.sprite = _cutInMagician;
                break;
            default:
                break;
        }
        if(_owner.GetPlayerID() == 1)
            _cutIn.GetComponent<Animator>().SetTrigger("Blue");
        else
            _cutIn.GetComponent<Animator>().SetTrigger("Red");
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
            case 0: //ICharacter.TYPE.FIGHTER:
                {
                    if (_possessionFighter <= 0) return;
                    _currentChara = Instantiate(_fighterPrefab).GetComponent<ICharacter>();
                }
                break;
            case 1: //ICharacter.TYPE.ARCHER:
                {
                    if (_possessionArcher <= 0) return;
                    _currentChara = Instantiate(_archerPrefab).GetComponent<ICharacter>();
                }
                break;
            case 2: //ICharacter.TYPE.MAGICIAN:
                {
                    if (_possessionMagician <= 0) return;
                    _currentChara = Instantiate(_magicianPrefab).GetComponent<ICharacter>();
                }
                break;
            default:
                break;
        }

        if (_owner.GetPlayerID() == 1)
            _currentChara.transform.position = new Vector3(3, 0, 0);
        else
        {
            _currentChara.transform.position = new Vector3(-3, 0, 0);
            //_currentChara.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }
    }

    public ICharacter SetCharacterOnBoard(ICharacter character)
    {
        switch (character._myType)
        {
            case ICharacter.TYPE.FIGHTER:
                _possessionFighter--;
                break;
            case ICharacter.TYPE.ARCHER:
                _possessionArcher--;
                break;
            case ICharacter.TYPE.MAGICIAN:
                _possessionMagician--;
                break;
            default:
                break;
        }
        character.SetOnBoard(true);
        _characters.Add(character);
        Instantiate(_summonEffect, character.transform.position, new Quaternion(0, 0, 0, 0));
        //SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("ExtraSFX"), false, "GenericGameSFX");

        switch (character.GetMyType())
        {
            case ICharacter.TYPE.FIGHTER:
                SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS,
                    AudioClipManager.GetInstance().GetAudioClip("WarriorAtk_Voice_2"), false, "GenericGameSFX");
                break;
            case ICharacter.TYPE.ARCHER:
                SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS,
                    AudioClipManager.GetInstance().GetAudioClip("ArcherAtk_Voice_2"), false, "GenericGameSFX");
                break;
            case ICharacter.TYPE.MAGICIAN:
                SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS,
                    AudioClipManager.GetInstance().GetAudioClip("Mage_Voice_2"), false, "GenericGameSFX");
                break;
        }
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
            case ICharacter.TYPE.FIGHTER:
                return _possessionFighter;
            case ICharacter.TYPE.ARCHER:
                return _possessionArcher;
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


    public void CharaVictory(ICharacter character)
    {
        character.Victory();
        switch (character._myType)
        {
            case ICharacter.TYPE.FIGHTER:
                character.SetSprite(_wonFighterSprite);
                break;
            case ICharacter.TYPE.ARCHER:
                character.SetSprite(_wonArcherSprite);
                break;
            case ICharacter.TYPE.MAGICIAN:
                character.SetSprite(_wonMagicianSprite);
                break;
            default:
                break;
        }
    }
    public void CharaLose(ICharacter character)
    {
        character.Defeated();
        DeleteCharacter(character);
    }

    public void DeleteCharacter(ICharacter character)
    {
        _characters.Remove(character);
        Destroy(character.gameObject);
    }
}
