using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : MonoBehaviour
{
    protected CPController charaController;
    int playerID;

    Button archerButton;
    Button fighterButton;
    Button magicianButton;

    public void Initialize(int id)
    {
        charaController = GetComponent<CPController>();
        charaController.SetOwner(this);
        playerID = id;
        if (id == 1)
        {
            archerButton = GameObject.FindWithTag("Archer").GetComponent<Button>();
            fighterButton = GameObject.FindWithTag("Fighter").GetComponent<Button>();
            magicianButton = GameObject.FindWithTag("Magician").GetComponent<Button>();
        }
        else if (id == 2)
        {
            archerButton = GameObject.FindWithTag("Archer2").GetComponent<Button>();
            fighterButton = GameObject.FindWithTag("Fighter2").GetComponent<Button>();
            magicianButton = GameObject.FindWithTag("Magician2").GetComponent<Button>();
        }
    }

    public virtual bool SelectCharacter(BoardController boardCon)
    {
        return true;
    }

    public virtual bool Action()
    {
        return true;
    }

    public virtual bool Battle()
    {
        return true;
    }

    public virtual void EndProcess()
    {

    }

    public int GetPlayerID()
    {
        return playerID;
    }
}
