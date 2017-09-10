using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPiece
{
    public enum CharClass
    {
        WARRIOR,
        ARCHER,
        MAGICIAN,
        ALL,
    };

    public enum STATE
    {
        NEUTRAL,
        GREEN,
        FROZEN,
    };

    bool onBoard, onMouse;
    int uniqueID, attackRange, moveRange, frozenTurns;
    CharClass classType;
    STATE currState;
    Vector2 position;
    
    Image imageSprite = null;

    public CharacterPiece()
    {
        onBoard = false;
        frozenTurns = -1;
        uniqueID = -1;
        attackRange = -1;
        moveRange = -1;
        classType = CharClass.ALL;
        position = new Vector2(-1, -1);
    }

    public void SetCharacterStats(int uniqueID, CharClass classType)
    {
        this.uniqueID = uniqueID;
        this.classType = classType;
        moveRange = 1;
        switch (classType)
        {
            case CharClass.WARRIOR:
            case CharClass.MAGICIAN:
                {
                    attackRange = 1;
                }
                break;

            case CharClass.ARCHER:
                {
                    attackRange = 2;
                }
                break;
        }
    }

    public void NewTurn()
    {
        if(currState == STATE.FROZEN)
        {
            frozenTurns -= 1;
            if (frozenTurns == 0)
            {
                currState = STATE.NEUTRAL;
                onBoard = false;
            }
        }
    }

    public void ToggleAbility()
    {
        switch (classType)
        {
            case CharClass.WARRIOR:
                break;

            case CharClass.MAGICIAN:
                break;
        }
    }

    public void UseAbility()
    {

    }

    //Setter(s)

    public void SetPosition(Vector2 position)
    {
        this.position = position;
    }

    public void ChangeState(STATE newState)
    {
        if (currState != newState)
        {
            currState = newState;
            switch (currState)
            {
                case STATE.GREEN:
                    imageSprite.color = new Color(0, 1, 0);
                    break;

                case STATE.NEUTRAL:
                    imageSprite.color = new Color(1, 1, 1);
                    break;
                    
                case STATE.FROZEN:
                    imageSprite.color = new Color(0, 0, 0);
                    break;
            }
        }
        
    }

    public void SetOnBoard(bool onBoard)
    {
        this.onBoard = onBoard;
    }


    //Getter(s)

    public Vector2 GetPosition()
    {
        return position;
    }

    public STATE GetCurrentState()
    {
        return currState;
    }

    public bool OnBoard()
    {
        return onBoard;
    }

    public CharClass GetClassType()
    {
        return classType;
    }


    // Mouse
    public bool OnMouse()
    {
        return onMouse;
    }

    private void OnMouseOver()
    {
        onMouse = true;
    }

    private void OnMouseExit()
    {
        onMouse = false;
    }

}
