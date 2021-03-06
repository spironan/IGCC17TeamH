﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AI : IPlayer
{

    // 現在の状態
    enum STATE
    {
        PUT,
        MOVE
    }

    struct Data
    {
        public STATE _state;
        public ICharacter.TYPE _type;
        public int _x; // 最終的にたどり着く場所
        public int _y;// 最終的にたどり着く場所
        public int _score;      // 評価
        public ICharacter _character;
    };


    List<Data> _data = new List<Data>();

    CharController _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<CharController>();
    }

    // 置けるマスを調べる
    void PutCheck(BoardController board)
    {
        Tile t = null;
        // 縦調べる
        // iがｙ
        for (int i = 0; i < board.GetHeight(); i++)
        {

            t = board.GetTile(i, 0);
            // 障害物がなければ次の処理
            if (t.OnPiece() == true) continue;

            t = board.SlideMove(0, i, 1, 0);
            // タイプ分回す
            for (int j = 0; j < 3; j++)
            {
                if (_charaController.GetPossessionCount((ICharacter.TYPE)j) <= 0) continue;
                
                // データを登録
                Data data = new Data();
                data._type = (ICharacter.TYPE)j;
                data._state = STATE.PUT;
                data._x = t.X();
                data._y = t.Y();
                data._score = SumEvaluation(data, board);
                _data.Add(data);
            }
        }
    }

    // 動かせるマスを調べる
    void MoveCheck(BoardController board)
    {
        Tile t = null;

        Vector2[] direction = new Vector2[4];
        direction[0] = new Vector2(-1, 0);
        direction[1] = new Vector2(1, 0);
        direction[2] = new Vector2(0, 1);
        direction[3] = new Vector2(0, -1);

        foreach (ICharacter character in _charaController.GetCharacters())
        {
            if (character.GetMyState() == ICharacter.STATE.FROZEN) continue;

            for (int i = 0; i < 4; i++)
            {
                t = board.SlideMove(character.X(), character.Y(), (int)direction[i].x, (int)direction[i].y);
                if (!t) continue;
                // 移動先のポジションと移動前のポジションが同じ
                if (t.X() == character.X() && t.Y() == character.Y()) continue;

                // データを登録
                Data data = new Data();
                data._type = character.GetMyType();
                data._state = STATE.MOVE;
                data._x = t.X();
                data._y = t.Y();
                data._score = SumEvaluation(data, board);
                data._character = character;
                _data.Add(data);
            }
        }
    }

    // 評価
    int SumEvaluation(Data d, BoardController board)
    {
        int score = 0;
        foreach (ICharacter character in _player.GetCharacters())
        {
            // バトルの相性を調べる
            int distance = BattleManager.Instance.GetDistance(d._x, d._y, character.X(), character.Y());
            BattleManager.Compatibility compaibliy = BattleManager.Instance.CheckCompatibility(d._type, character.GetMyType());
            if (distance == 1)
            {
                // スコアの割り当て
                if (compaibliy == BattleManager.Compatibility.Strong) score += 30;
                if (compaibliy == BattleManager.Compatibility.Weak) score -= 15;
            }
            if (d._character && d._character.GetMyState() == ICharacter.STATE.GREEN)
            {
                score -= 20;
            }

            if (d._x == character.X())
            {
                int vecY = (character.Y() - d._y > 0) ? 1 : -1;
                Tile tile = board.SlideMove(d._x, d._y, 0, vecY);
                if (tile.Y() + vecY == character.Y())
                {
                    if (compaibliy == BattleManager.Compatibility.Strong) score += 5;
                    else if (compaibliy == BattleManager.Compatibility.Weak) score -= 5;
                    else score += 3;
                    if (d._character)
                    {
                        if (d._character.X() == d._x)
                        {
                            if (compaibliy == BattleManager.Compatibility.Strong) score += 5;
                            else if (compaibliy == BattleManager.Compatibility.Weak) score -= 5;
                        }
                    }
                }
            }
            if (d._y == character.Y())
            {
                int vecX = (character.X() - d._x > 0) ? 1 : -1;
                Tile tile = board.SlideMove(d._x, d._y, vecX, 0);
                if (tile.X() + vecX == character.X())
                {
                    if (compaibliy == BattleManager.Compatibility.Strong) score += 5;
                    else if (compaibliy == BattleManager.Compatibility.Weak) score -= 5;
                    else score += 3;
                    if (d._character)
                    {
                        if (d._character.Y() == d._y)
                        {
                            if (compaibliy == BattleManager.Compatibility.Strong) score += 5;
                            else if (compaibliy == BattleManager.Compatibility.Weak) score -= 5;
                        }
                    }
                }
            }
        }

        for (int i = -1; i <= 1; i++)
        {
            int y = d._y + i;
            Tile tile = board.GetTile(y, board.GetWidth() - 1);
            if (!tile || tile.OnPiece()) continue;
            tile = board.SlideMove(tile.X(), tile.Y(), -1, 0);
            if (BattleManager.Instance.GetDistance(tile.X(), tile.Y(), d._x, d._y) <= 1)
            {
                score -= 10;
                break;
            }
        }

        #region
        // 置いた場所で勝てるかどうか
        //
        //
        //for (int i = 0; i < board.GetHeight(); i++)
        //{
        //    for (int j = 0; j < board.GetWidth(); j++)
        //    {
        //        Tile t = board.GetTile(i, j);
        //        if (t == null) score += 3;
        //    }
        //}
        #endregion  

        return score;
    }

    // リストの中身をソート
    Data SortEva()
    {
        _data.Sort((a, b) => b._score - a._score);
        return _data[0];
    }

    bool _isStop = false;
    IEnumerator Wait(float seconds)
    {
        _isStop = true;
        yield return new WaitForSeconds(seconds);
        _isStop = false;
    }

    // キャラの設置
    public override bool SelectCharacter(BoardController boardCon)
    {
        ICharacter character = null;
        Tile tile = null;
        //Data data= new Data();
        if (!_charaController.GetCurrentCharacter())
        {
            PutCheck(boardCon);
            MoveCheck(boardCon);
            SortEva();
            if (_data.Count >= 3)
            {
                int elem = Random.Range(0, 3);
                elem = (elem != 0) ? 0 : 1;
                _data[0] = _data[elem];
            }
        }
        if (_isStop)
        {
            return false;
        }

        if(_data.Count == 0)
        {
            return true;
        }


        /// キャラを新規投入か既存キャラの移動かを決める
        // 新規に追加する場合
        // ソートした結果が操作キャラ
        if (_data[0]._state == STATE.PUT && !_charaController.GetCurrentCharacter())
        // 新規のキャラクターを動かす処理------
        {
            // （評価を元に）
            // どのキャラを動かすか選択
            _charaController.Generation((int)_data[0]._type);
            // 現在のキャラとして代入
            character = _charaController.GetCurrentCharacter();
            // 移動先のポジション
            tile = boardCon.GetTile(_data[0]._y, 0);
            //tile = boardCon.GetTile(0, current._x);
            // ボード上に追加
            character.transform.position = tile.transform.position;     //
            _charaController.SetCharacterOnBoard(character);
            StartCoroutine(Wait(1.0f));
            return false;
        }//------
        else if (_data[0]._state == STATE.MOVE)
        // 既存のキャラを動かす処理------
        {
            _charaController.SetCurrentCharacter(_data[0]._character);
            // 現在のキャラとして代入
            character = _charaController.GetCurrentCharacter();
            // 一度false 
            boardCon.GetTile(character.Y(), character.X()).OnPiece(false);
        }


        // 共通の処理
        // キャラを動かす処理
        character = _charaController.GetCurrentCharacter();
        if(_data[0]._state == STATE.PUT)
            character.transform.position = boardCon.GetTile(_data[0]._y, 0).transform.position;
        Tile moveTo = boardCon.GetTile(_data[0]._y, _data[0]._x);
        StartCoroutine(character.ConstantMove(moveTo.transform.position, 10));
        // 内部的な座標の設定
        character.SetPosition(moveTo.X(), moveTo.Y());
        // タイルに乗っているか（乗せた）
        moveTo.OnPiece(true);
        _charaController.IsPlaying(false);

        for (int i = _data.Count - 1; i >= 0; i--)
        {
            _data.RemoveAt(i);
        }
        return true;
    }

    public override bool Action()
    {
        return true;
    }

    public override bool Battle()
    {
        return true;
    }

    public override void EndProcess()
    {

    }
}
    

