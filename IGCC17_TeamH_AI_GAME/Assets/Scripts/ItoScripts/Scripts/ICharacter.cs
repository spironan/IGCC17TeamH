using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICharacter : MonoBehaviour {

    public enum TYPE
    {
        FIGHTER,
        ARCHER,
        MAGICIAN
    }

    public enum STATE
    {
        NEUTRAL,
        GREEN,
        FROZEN,
    };
    
    public TYPE _myType = TYPE.FIGHTER;
    STATE _state = STATE.NEUTRAL;
    int _moveRange, _attackRange;
    int _x, _y;
    bool _onMouse, _onBoard;
    SpriteRenderer _renderer;
    BoxCollider _collider;

    private void Start()
    {
        // Temporary placement
        // 仮置き
        SetPosition(-1, -1);
        _moveRange = _attackRange = 1;
        _onMouse = _onBoard = false;
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider>();
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

    public void Defeated()
    {
        if (_state == STATE.GREEN) return;
        _state = STATE.FROZEN;
        _renderer.color = new Color(0.3f, 0.3f, 0.3f, 1);
        _collider.enabled = false;
    }

    public void Victory()
    {
        _state = STATE.GREEN;
        _renderer.color = new Color(0, 1, 0, 1);
        _collider.enabled = false; // should this be true
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
