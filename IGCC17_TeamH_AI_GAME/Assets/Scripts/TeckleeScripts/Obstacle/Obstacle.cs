using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    int _x, _y;
    SpriteRenderer _renderer;
    bool _dontDestroy;

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

    public void SetRenderer(SpriteRenderer renderer)
    {
        _renderer = renderer;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return _renderer;
    }

    public void DontDestroy(bool isDestroy)
    {
        _dontDestroy = isDestroy;
    }

    public bool DontDestroy()
    {
        return _dontDestroy;
    }
}
