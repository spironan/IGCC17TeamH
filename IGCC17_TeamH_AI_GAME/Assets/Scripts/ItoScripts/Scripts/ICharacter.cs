using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICharacter : MonoBehaviour {

    public enum TYPE
    {
        ARCHER,
        FIGHTER,
        MAGICIAN
    }

    public enum STATE
    {
        NEUTRAL,
        GREEN,
        FROZEN,
    };
    
    public TYPE _myType = TYPE.ARCHER;
    STATE _state = STATE.NEUTRAL;
    int _moveRange, _attackRange;
    int _x, _y;
    bool _onMouse, _onBoard;

    private void Start()
    {
        // Temporary placement
        // 仮置き
        //switch (_myType)
        //{
        //    case TYPE.ARCHER:
        //        break;
        //    case TYPE.FIGHTER:
        //        break;
        //    case TYPE.MAGICIAN:
        //        break;
        //    default:
        //        break;
        //}
        _myType = TYPE.ARCHER;
        _moveRange = 1;
        _attackRange = 1;
        SetPosition(-1, -1);
        _onMouse = _onBoard = false;
    }

    public TYPE GetMyType()
    {
        return _myType;
    }

    public STATE GetMyState()
    {
        return _state;
    }
    public void ChangeState(STATE newState)
    {
        _state = newState;
    }

    public int GetMoveRange()
    {
        return _moveRange;
    }

    public int GetAttackRange()
    {
        return _attackRange;
    }

    public int X()
    {
        return _x;
    }
    public int Y()
    {
        return _y;
    }
    public void SetPosition(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public bool OnMouse()
    {
        return _onMouse;
    }
    private void OnMouseOver()
    {
        _onMouse = true;
    }
    private void OnMouseExit()
    {
        _onMouse = false;
    }

    public bool OnBoard()
    {
        return _onBoard;
    }
    public void SetOnBoard(bool onBoard)
    {
        _onBoard = onBoard;
    }

    public IEnumerator ConstantMove(Vector3 goal, int flame)
    {
        Vector3 vel = (goal - transform.position) / flame;
        for (int i = 0; i < flame; i++)
        {
            transform.position += vel;
            yield return null;
        }
    }
}
