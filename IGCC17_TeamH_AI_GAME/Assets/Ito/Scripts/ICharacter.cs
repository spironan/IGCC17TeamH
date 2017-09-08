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

    TYPE _myType;
    int _moveRange, _attackRange;
    int _x, _y;
    bool _onMouse;

    private void Start()
    {
        // Temporary placement
        // 仮置き
        _myType = TYPE.ARCHER;
        _moveRange = 1;
        _attackRange = 2;
        SetPosition(-1, -1);
    }

    public TYPE GetMyType()
    {
        return _myType;
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
}
