using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPController : MonoBehaviour
{
    //bool takeAction = false;

    List<CharacterPiece> characters = new List<CharacterPiece>();
    CharacterPiece currentChara = null;

    //int pieceID = 0;
    int possessionArcher;
    int possessionWarrior;
    int possessionMagician;

    GameObject archerPrefab;
    GameObject warriorPrefab;
    GameObject magicianPrefab;

    PlayerBase owner;
    
    // Use this for initialization
    void Start ()
    {
        possessionArcher = 2;
        possessionWarrior = 2;
        possessionMagician = 2;
        currentChara = null;

        archerPrefab = Resources.Load("Prefab/ICharacter") as GameObject;
        warriorPrefab = Resources.Load("Prefab/ICharacter") as GameObject;
        magicianPrefab = Resources.Load("Prefab/ICharacter") as GameObject;
        
        //int maxAmount = 2;
        //for (CharacterPiece.CharClass classType = CharacterPiece.CharClass.WARRIOR;
        //    classType < CharacterPiece.CharClass.ALL; ++classType)
        //{
        //    for (int amount = 0; amount < maxAmount; ++amount)
        //    {
        //        CharacterPiece newChar = new CharacterPiece();
        //        newChar.SetCharacterStats(pieceID, classType);
        //        characters.Add(newChar);
        //        ++pieceID;
        //    }
        //}
	}

    public void SetOwner(PlayerBase owner)
    {
        this.owner = owner;
    }

    public CharacterPiece GetCurrentCharacter()
    {
        return currentChara;
    }

    public void SetCurrentCharacter(CharacterPiece chara)
    {
        currentChara = chara;
    }

    public CharacterPiece GetOnMouseCharacter()
    {
        foreach (CharacterPiece chara in characters)
        {
            if (chara.OnMouse())
            {
                return chara;
            }
        }
        return null;
    }

    public void StartTurn()
    {
        //takeAction = false;
        foreach (CharacterPiece charPiece in characters)
        {
            charPiece.NewTurn();
        }
    }
    
    public CharacterPiece GenerateChar(CharacterPiece.CharClass classType)
    {
        switch (classType)
        {
            case CharacterPiece.CharClass.WARRIOR:
                {
                    if (possessionWarrior > 0)
                    {
                        currentChara = Instantiate(warriorPrefab).GetComponent<CharacterPiece>();
                        possessionWarrior--;
                        characters.Add(currentChara);
                    }
                }
                break;
            case CharacterPiece.CharClass.ARCHER:
                {
                    if (possessionArcher > 0)
                    {
                        currentChara = Instantiate(archerPrefab).GetComponent<CharacterPiece>();
                        possessionArcher--;
                        characters.Add(currentChara);
                    }
                }
                break;
            case CharacterPiece.CharClass.MAGICIAN:
                {
                    if (possessionMagician > 0)
                    {
                        currentChara = Instantiate(magicianPrefab).GetComponent<CharacterPiece>();
                        possessionMagician--;
                        characters.Add(currentChara);
                    }
                }
                break;
        }
        
        return currentChara;
    }
    
    //Win Condition Check
    public bool GetCharTypeGreen(CharacterPiece.CharClass classType)
    {
        foreach (CharacterPiece charPiece in characters)
        {
            if (charPiece.GetClassType() == classType && charPiece.OnBoard() && charPiece.GetCurrentState() == CharacterPiece.STATE.GREEN)
            {
                return true;
            }
        }
        return false;
    }
    
}
