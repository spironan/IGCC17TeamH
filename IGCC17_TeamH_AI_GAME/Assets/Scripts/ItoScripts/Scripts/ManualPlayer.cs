using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualPlayer : IPlayer {

    OperationUI _operationUI;

    GameObject _summonEffect;
    private void Start()
    {
        _charaController = gameObject.GetComponent<CharController>();
        _operationUI = GameObject.Find("OperationUI").GetComponent<OperationUI>();
        _summonEffect = Resources.Load("Prefab/SummonEffect") as GameObject;
    }

    public override bool SelectCharacter(BoardController boardCon)
    {
        ICharacter character = _charaController.GetCurrentCharacter();
        // track ui
        if (character)
            _operationUI.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(Camera.main, character.transform.position);
        // wait for button
        if (!Input.GetMouseButtonDown(0)) return false;
        // have character is delete
        if (_charaController.GetOnMouseCharacter())
        {
            if (character && character.X() == -1)
            {
                Destroy(character.gameObject);
            }
            character = _charaController.GetOnMouseCharacter();
            _charaController.SetCurrentCharacter(character);
        }
        if (character)
        {
            Tile tile = boardCon.GetOnMouseTile();
            // tile on not piece
            if (tile && !tile.OnPiece())
            {
                // new character
                if (character.X() == -1)
                {
                    if (GetPlayerID() == 1 && tile.X() != boardCon.GetWidth() - 1) return false;
                    if (GetPlayerID() == 2 && tile.X() != 0) return false;
                    character.transform.position = tile.transform.position;
                    _charaController.SetCharacterOnBoard(character);
                    Instantiate(_summonEffect, character.transform.position, new Quaternion(0, 0, 0, 0));
                    if (GetPlayerID() == 1)
                        tile = boardCon.SlideMove(tile.X(), tile.Y(), -1, 0);
                    else
                        tile = boardCon.SlideMove(tile.X(), tile.Y(), 1, 0);
                }
                // not new character
                else
                {
                    if (character.X() + 1 == tile.X() && character.Y() == tile.Y())
                        tile = boardCon.SlideMove(character.X(), character.Y(), 1, 0);
                    else if (character.X() - 1 == tile.X() && character.Y() == tile.Y())
                        tile = boardCon.SlideMove(character.X(), character.Y(), -1, 0);
                    else if (character.X() == tile.X() && character.Y() + 1 == tile.Y())
                        tile = boardCon.SlideMove(character.X(), character.Y(), 0, 1);
                    else if (character.X() == tile.X() && character.Y() - 1 == tile.Y())
                        tile = boardCon.SlideMove(character.X(), character.Y(), 0, -1);
                    else
                        return false;
                    boardCon.GetTile(character.Y(), character.X()).OnPiece(false);
                }

                StartCoroutine(character.ConstantMove(tile.transform.position, 10));
                character.SetPosition(tile.X(), tile.Y());
                tile.OnPiece(true);
                _charaController.IsPlaying(false);
                return true;
            }
        }
        return false;
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
