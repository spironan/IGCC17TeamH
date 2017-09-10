using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualPlayer : IPlayer {

    OperationUI _operationUI;
    private void Start()
    {
        _charaController = gameObject.GetComponent<CharController>();
        _operationUI = GameObject.Find("OperationUI").GetComponent<OperationUI>();
    }

    public override bool SelectCharacter(BoardController boardCon)
    {
        ICharacter character = _charaController.GetCurrentCharacter();
        if(character)
            _operationUI.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(Camera.main, character.transform.position);
        if (!Input.GetMouseButtonDown(0)) return false;

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
            if (tile && !tile.OnPiece())
            {
                if (GetPlayerID() == 1 && tile.X() != boardCon.GetWidth() - 1) return false;
                if (GetPlayerID() == 2 && tile.X() != 0) return false;
                if (character.X() == -1)
                {
                    _charaController.GetCharacters().Add(character);
                }
                else
                {
                    boardCon.GetTile(character.Y(), character.X()).OnPiece(false);
                }
                character.transform.position = tile.transform.position;
                character.SetPosition(tile.X(), tile.Y());
                tile.OnPiece(true);
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
