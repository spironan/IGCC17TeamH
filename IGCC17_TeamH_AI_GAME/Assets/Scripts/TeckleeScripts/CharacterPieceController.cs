using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPieceController : MonoBehaviour {

    public enum TEAM
    {
        PLAYER,
        AI,
    };

    bool takeAction = false;
    int maxAmount = 2;
    public TEAM team;
    List<CharacterPiece> characters;
    
	// Use this for initialization
	void Start () {
        int uniqueID = 0;
        for (CharacterPiece.CharClass classType = CharacterPiece.CharClass.WARRIOR;
            classType < CharacterPiece.CharClass.ALL; ++classType)
        {
            for (int amount = 0; amount < maxAmount; ++amount)
            {
                CharacterPiece newChar = new CharacterPiece();
                newChar.SetCharacterStats(uniqueID, classType);
                characters.Add(newChar);
                ++uniqueID;
            }
        }
	}
    
    public void StartTurn()
    {
        takeAction = false;
        foreach (CharacterPiece charPiece in characters)
        {
            charPiece.NewTurn();
        }
    }

    public CharacterPiece GetCharacterFromUI(CharacterPiece.CharClass classType)
    {
        foreach (CharacterPiece charPiece in characters)
        {
            if (charPiece.GetClassType() == classType && !charPiece.OnBoard())
            {
                return charPiece;
            }
        }

        return null;
    }

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

    public void SetTeam(TEAM team)
    {
        this.team = team;
    }




}
