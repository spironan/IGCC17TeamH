using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    enum GAME_CONDITION
    {
        SELECT,
        ACTION,
        BATTLE,
        ENDPROCESS
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
    
	// Use this for initialization
	void Start () {
        _gameCondition = GAME_CONDITION.SELECT;
        _player1 = _player1Object.AddComponent<ManualPlayer>();
        _player2 = _player2Object.AddComponent<ManualPlayer>();
        _player1.Initialize(1);
        _player2.Initialize(2);
        _currentPlayer = _player1;

        _currentPlayer.GetCharController().IsPlaying(true);
    }
	
	// Update is called once per frame
	void Update () {
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
            default:
                break;
        }
	}
    
    private void SelectCharacter()
    {
        if(_currentPlayer.SelectCharacter(_boardController))
        {
            _gameCondition = GAME_CONDITION.ACTION;
        }
    }

    private void Action()
    {
        IPlayer defender = (_currentPlayer == _player1) ? _player2 : _player1;
        BattleManager.Instance.Battle(_currentPlayer, defender);
        _gameCondition = GAME_CONDITION.BATTLE;
    }

    private void Battle()
    {
        // winner is next turn
        // if currentPlayer is lose
        _currentPlayer = (_currentPlayer == _player1) ? _player2 : _player1;
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
    }
}
