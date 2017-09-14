using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GAME_CONDITION
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

    GameEndDisplayScript _endScreen;

    bool _isStop;

    // Use this for initialization
    void Start()
    {
        SoundSystem.Instance.PlayClip(AUDIO_TYPE.BACKGROUND_MUSIC, AudioClipManager.GetInstance().GetAudioClip("GameSceneBGM"), true, "BGMSource");

        _gameCondition = GAME_CONDITION.SELECT;
        _player1 = _player1Object.AddComponent<ManualPlayer>();
        _player2 = _player2Object.AddComponent<AI>();
        _player1.Initialize(1);
        _player2.Initialize(2);
        _currentPlayer = _player1;

        _currentPlayer.GetCharController().IsPlaying(true);

        _boardController.AddObstacle(1, 1, true);
        _boardController.AddObstacle(3, 3, true);

        _endScreen = GameObject.Find("EndScreenDisplay").GetComponent<GameEndDisplayScript>();
        _isStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_turnCount <= 0 || _endScreen.genericDisplay.active)
        {
            _gameCondition = GAME_CONDITION.RESULT;
        }
        if (_isStop) return;

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
                StartCoroutine(EndProcess());
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
        // バトルアニメーション再生
        // カットイン挿入
        // バトル結果を出力
        // 結果に応じて敗北アニメーション
        if (BattleManager.Instance._isBattle == BattleManager.BATTLE_STATE.None)
        {
            IPlayer defender = (_currentPlayer == _player1) ? _player2 : _player1;
            StartCoroutine(BattleManager.Instance.BattleFlow(_currentPlayer, defender, _boardController));
        }
        else if(BattleManager.Instance._isBattle == BattleManager.BATTLE_STATE.Finished)
        {
            _currentPlayer = (_currentPlayer == _player1) ? _player2 : _player1;
            _gameCondition = GAME_CONDITION.ENDPROCESS;
            BattleManager.Instance._isBattle = BattleManager.BATTLE_STATE.None;
        }
    }

    private IEnumerator EndProcess()
    {
        _turnCount--;

        // break obstacle
        _boardController.RandomDestroyObstacle();
        _isStop = true;
        yield return new WaitForSeconds(2.0f);
        _isStop = false;
        _gameCondition = GAME_CONDITION.SELECT;
        _player1.GetCharController().SetCurrentCharacter(null);
        _player2.GetCharController().SetCurrentCharacter(null);

        _player1.GetCharController().IsPlaying(false);
        _player2.GetCharController().IsPlaying(false);
        _currentPlayer.GetCharController().IsPlaying(true);
    }

    private void Result()
    {
        // result screen create
        _player1.GetCharController().SetCurrentCharacter(null);
        _player2.GetCharController().SetCurrentCharacter(null);
        _player1.GetCharController().IsPlaying(false);
        _player2.GetCharController().IsPlaying(false);
    }

    public int GetTurnNumber()
    {
        return _turnCount;
    }

    public void Pause(bool toPause)
    {
        _isStop = toPause;
    }

    public GAME_CONDITION GetGameCondition()
    {
        return _gameCondition;
    }
}
