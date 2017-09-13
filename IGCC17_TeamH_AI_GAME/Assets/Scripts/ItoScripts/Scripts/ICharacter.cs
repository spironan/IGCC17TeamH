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

    public enum CONDITION
    {
        WAIT,
        ACTION,
        END
    }

    public TYPE _myType = TYPE.FIGHTER;
    STATE _state = STATE.NEUTRAL;
    CONDITION _condition;
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
        _condition = CONDITION.WAIT;
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

    public void ChangeCondition(CONDITION condition)
    {
        _condition = condition;
    }

    public CONDITION GetCondition()
    {
        return _condition;
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

    public void SetSprite(Sprite newSprite)
    {
        _renderer.sprite = newSprite;
    }


    public void Defeated()
    {
        if (_state == STATE.GREEN) return;
        _state = STATE.FROZEN;
        //_renderer.color = new Color(0.3f, 0.3f, 0.3f, 1);
        _collider.enabled = false;
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    public void Victory()
    {
        _state = STATE.GREEN;
        //_renderer.color = new Color(0, 1, 0, 1);
        _collider.enabled = true; // should this be true
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    public IEnumerator ConstantMove(Vector3 goal, int flame)
    {
        _condition = CONDITION.ACTION;
        if (_x == -1)
        {
            yield return new WaitForSeconds(1.0f);
        }
        Vector3 vel = (goal - transform.position) / flame;      //
        for (int i = 0; i < flame; i++)
        {
            transform.position += vel;      //
            yield return null;
        }
        _condition = CONDITION.END;
    }
}
