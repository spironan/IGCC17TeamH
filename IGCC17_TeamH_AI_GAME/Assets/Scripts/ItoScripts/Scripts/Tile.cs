using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField]
    bool _onPiece;
    bool _onMouse;
    int _x, _y;
    SpriteRenderer _spriteRenderer;

	void Awake () {
        _onPiece = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetColor(new Color(1, 1, 1, 0.4f));
    }
    
    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    public bool OnPiece()
    {
        return _onPiece;
    }
    public void OnPiece(bool exist)
    {
        _onPiece = exist;
    }

    public bool OnMouse()
    {
        return _onMouse;
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

    private void OnMouseOver()
    {
        _onMouse = true;
    }

    private void OnMouseExit()
    {
        _onMouse = false;
    }
}
