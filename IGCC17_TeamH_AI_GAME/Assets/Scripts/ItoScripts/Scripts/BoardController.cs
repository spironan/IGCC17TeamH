﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

    // SerializeField
    [SerializeField]
    int _width;
    [SerializeField]
    int _height;
    [SerializeField]
    GameObject _tilePrefab;
    [SerializeField]
    float _tileSize;

    Tile[,] _tileTable;

	// Use this for initialization
	void Start () {
        _tileTable = new Tile[_height, _width];
        for(int i = 0;i < _height;i++)
        {
            for(int j = 0;j < _width;j++)
            {
                GameObject tile = Instantiate(_tilePrefab);
                tile.transform.position = new Vector3(j * _tileSize, i * _tileSize, 0)
                                        - new Vector3((_width - 1) * _tileSize / 2, (_height - 1) * _tileSize / 2, 0);
                _tileTable[i, j] = tile.GetComponent<Tile>();
                _tileTable[i, j].SetPosition(j, i);
            }
        }
        _tileTable[1, 1].OnPiece(true);
        _tileTable[3, 3].OnPiece(true);
    }

    public Tile GetTile(int h, int w)
    {
        if (h < 0 || h >= _height || w < 0 || w >= _width)
            return null;
        return _tileTable[h, w];
    }

    public int GetHeight()
    {
        return _height;
    }
    public int GetWidth()
    {
        return _width;
    }

    public Tile GetOnMouseTile()
    {
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                if (_tileTable[i, j].OnMouse())
                    return _tileTable[i, j];
            }
        }
        return null;
    }

    public Tile SlideMove(int startX, int startY, int vecX, int vecY)
    {
        int x = startX;
        int y = startY;

        do
        {
            x += vecX;
            y += vecY;
            if (GetTile(y, x) && GetTile(y, x).OnPiece())
            {
                break;
            }
        } while (y >= 0 && y < _height && x >= 0 && x < _width);

        x -= vecX;
        y -= vecY;
        return GetTile(y, x);
    }

    public void TileColorChange(IPlayer player)
    {
        ICharacter character = player.GetCharController().GetCurrentCharacter();
        for(int i = 0;i < _height;i++)
        {
            for(int j = 0;j < _width;j++)
            {

            }
        }
        if(character.X() == -1)
        {
            if(player.GetPlayerID() == 1)
            {

            }
            else
            {

            }
        }
        else
        {

        }
    }
}
