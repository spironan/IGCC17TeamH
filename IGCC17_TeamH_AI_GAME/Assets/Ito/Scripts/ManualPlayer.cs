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
        if (!Input.GetMouseButtonDown(0)) return false;

        ICharacter character = _charaController.GetCurrentCharacter();
        if (_charaController.GetOnMouseCharacter())
        {
            if (character && character.X() == -1)
            {
                Destroy(character.gameObject);
            }
            character = _charaController.GetOnMouseCharacter();
            _charaController.SetCurrentCharacter(character);
            return false;
        }
        if (character)
        {
            Tile tile = boardCon.GetOnMouseTile();
            if (tile && !tile.OnPiece())
            {
                if (character.X() == -1)
                {
                    _charaController.GetCharacters().Add(character);
                }
                character.transform.position = tile.transform.position;
                character.SetPosition(tile.X(), tile.Y());
                //return true;
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
