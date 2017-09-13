using System.Collections;
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

    [SerializeField]
    GameObject _obstaclePrefab;
    List<Obstacle> _obstacles = new List<Obstacle>();

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
        Tile onMouseTile = null;
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                if (character)
                    _tileTable[i, j].SetColor(new Color(0.5f, 0.2f, 0.1f, 0.5f));
                else
                    _tileTable[i, j].SetColor(new Color(0.5f, 0.5f, 0.5f, 0.0f));

                if (_tileTable[i, j].OnMouse())
                {
                    onMouseTile = _tileTable[i, j];
                }
            }
        }

        if (character && character.X() == -1)
        {
            if (player.GetPlayerID() == 1)
            {
                for (int i = 0; i < _height; i++)
                {
                    _tileTable[i, _width - 1].SetColor(new Color(0.0f, 0.0f, 1.0f, 0.5f));
                }
            }
            else
            {
                for (int i = 0; i < _height; i++)
                {
                    _tileTable[i, 0].SetColor(new Color(0.0f, 0.0f, 1.0f, 0.5f));
                }
            }
        }
        else if (character)
        {
            Tile tile = GetTile(character.Y(), character.X() + 1);
            if (tile && !tile.OnPiece()) tile.SetColor(new Color(0.0f, 0.0f, 1.0f, 0.5f));
            tile = GetTile(character.Y(), character.X() - 1);
            if (tile && !tile.OnPiece()) tile.SetColor(new Color(0.0f, 0.0f, 1.0f, 0.5f));
            tile = GetTile(character.Y() + 1, character.X());
            if (tile && !tile.OnPiece()) tile.SetColor(new Color(0.0f, 0.0f, 1.0f, 0.5f));
            tile = GetTile(character.Y() - 1, character.X());
            if (tile && !tile.OnPiece()) tile.SetColor(new Color(0.0f, 0.0f, 1.0f, 0.5f));
        }
        if (onMouseTile)
        {
            onMouseTile.SetColor(new Color(1, 1, 0, 0.5f));
        }
    }

    public List<Obstacle> GetObstacles()
    {
        return _obstacles;
    }

    public void AddObstacle(int x, int y, bool dontDestroy = false)
    {
        Obstacle obs = Instantiate(_obstaclePrefab).GetComponent<Obstacle>();
        obs.SetPosition(x, y);
        GetTile(y, x).OnPiece(true);
        obs.transform.position = GetTile(y, x).transform.position;
        obs.DontDestroy(dontDestroy);

        _obstacles.Add(obs);
    }

    public void RandomDestroyObstacle()
    {
        int obstacleCount = _obstacles.Count;
        if (obstacleCount < 5) return;
        for(int i = 0;i < 1000;i++)
        {
            if (obstacleCount == 0) break;
            int element = Random.Range(0, obstacleCount);
            if (_obstacles[element].DontDestroy()) continue;
            GetTile(_obstacles[element].Y(), _obstacles[element].X()).OnPiece(false);
            Destroy(_obstacles[element].gameObject);
            _obstacles.RemoveAt(element);
            break;
        }
    }
}