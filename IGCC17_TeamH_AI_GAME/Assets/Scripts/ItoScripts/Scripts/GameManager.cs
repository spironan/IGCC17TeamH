using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    enum GAME_CONDITION
    {
        SELECT,
        ACTION,
        BATTLE,
        ENDPROCESS,
        RESULT
    }

    [SerializeField]
    GameObject _player1Object;
    [SerializeField]
    GameObject _player2Object;
    [SerializeField]
    BoardController _boardController;

    GAME_CONDITION _gameCondition;
    IPlayer _player1;
    IPlayer _player2;
    IPlayer _currentPlayer;
    [SerializeField]
    int _turnCount;

    // Use this for initialization
    void Start()
    {
        _gameCondition = GAME_CONDITION.SELECT;
        _player1 = _player1Object.AddComponent<ManualPlayer>();
        _player2 = _player2Object.AddComponent<ManualPlayer>();
        _player1.Initialize(1);
        _player2.Initialize(2);
        _currentPlayer = _player1;

        _currentPlayer.GetCharController().IsPlaying(true);

        GameObject obstaclePrefab = Resources.Load("Prefab/Obstacle") as GameObject;
        Tile tile = _boardController.GetTile(1, 1);
        tile.OnPiece(true);
        Instantiate(obstaclePrefab, tile.transform.position, new Quaternion(0, 0, 0, 0));
        tile = _boardController.GetTile(3, 3);
        tile.OnPiece(true);
        Instantiate(obstaclePrefab, tile.transform.position, new Quaternion(0, 0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (_turnCount <= 0)
        {
            _gameCondition = GAME_CONDITION.RESULT;
        }

        switch (_gameCondition)
        {
            case GAME_CONDITION.SELECT:
                SelectCharacter();
                break;
            case GAME_CONDITION.ACTION:
                Action();
                break;
            case GAME_CONDITION.BATTLE:
                Battle();
                break;
            case GAME_CONDITION.ENDPROCESS:
                EndProcess();
                break;
            case GAME_CONDITION.RESULT:
                Result();
                break;
            default:
                break;
        }
    }

    private void SelectCharacter()
    {
        _boardController.TileColorChange(_currentPlayer);
        if (_currentPlayer.SelectCharacter(_boardController))
        {
            _gameCondition = GAME_CONDITION.ACTION;
        }
    }

    private void Action()
    {
        ICharacter character = _currentPlayer.GetCharController().GetCurrentCharacter();
        if (character.GetCondition() == ICharacter.CONDITION.END)
        {
            character.ChangeCondition(ICharacter.CONDITION.WAIT);
            _gameCondition = GAME_CONDITION.BATTLE;
        }
    }

    private void Battle()
    {
        IPlayer defender = (_currentPlayer == _player1) ? _player2 : _player1;
        if (BattleManager.Instance.Battle(_currentPlayer, defender))
        {
            // winner is next turn
            // if currentPlayer is lose
            _currentPlayer = (_currentPlayer == _player1) ? _player2 : _player1;
        }
        _gameCondition = GAME_CONDITION.ENDPROCESS;
    }

    private void EndProcess()
    {
        _gameCondition = GAME_CONDITION.SELECT;
        _player1.GetCharController().SetCurrentCharacter(null);
        _player2.GetCharController().SetCurrentCharacter(null);

        _player1.GetCharController().IsPlaying(false);
        _player2.GetCharController().IsPlaying(false);
        _currentPlayer.GetCharController().IsPlaying(true);

        _turnCount--;
    }

    private void Result()
    {
        // result screen create
    }

    public int GetTurnNumber()
    {
        return _turnCount;
    }
}
