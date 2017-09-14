using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{

    public enum Compatibility
    {
        Usually,
        Weak,
        Strong
    };

    public enum BATTLE_STATE
    {
        None,
        Play,
        Finished
    }


    public BATTLE_STATE _isBattle;
    static BattleManager _instance;

    public static BattleManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BattleManager();
            }
            return _instance;
        }
    }

    BattleManager()
    {
        _isBattle = BATTLE_STATE.None;
    }

    public IEnumerator BattleFlow(IPlayer challenger, IPlayer defender, BoardController board)
    {
        _isBattle = BATTLE_STATE.Play;
        ICharacter challengerChar = challenger.GetCharController().GetCurrentCharacter();
        if (challengerChar.GetMyState() == ICharacter.STATE.GREEN)
        {
            _isBattle = BATTLE_STATE.Finished;
            yield break;
        }
        List<ICharacter> defList = new List<ICharacter>();
        foreach(ICharacter character in defender.GetCharController().GetCharacters())
        {
            if (character.GetMyState() == ICharacter.STATE.FROZEN) continue;
            if (GetDistance(challengerChar, character) > challengerChar.GetAttackRange()) continue;
            if (challengerChar.GetMyType() == character.GetMyType()) continue;
            defList.Add(character);
        }
        if(defList.Count == 0)
        {
            _isBattle = BATTLE_STATE.Finished;
            yield break;
        }
        // 戦闘アニメーションを再生する
        foreach(ICharacter character in defList)
        {
            character.AttackAnimation(true);
        }
        challengerChar.AttackAnimation(true);
        // カットイン挿入
        challenger.GetCharController().PlayCutIn(challengerChar);
        yield return new WaitForSeconds(3.0f);
        // 戦闘アニメーションを終了する
        foreach (ICharacter character in defList)
        {
            character.AttackAnimation(false);
        }
        challengerChar.AttackAnimation(false);
        // 戦わせる
        for (int i = defList.Count - 1; i >= 0; --i)
        {
            if (CheckCompatibility(challengerChar.GetMyType(), defList[i].GetMyType()) == Compatibility.Strong)
            {
                board.AddObstacle(defList[i].X(), defList[i].Y());
                challenger.GetCharController().CharaVictory(challengerChar);
                defender.GetCharController().CharaLose(defList[i]);
            }
            else
            {
                if (defList[i].GetMyState() == ICharacter.STATE.GREEN) continue;
                board.AddObstacle(challengerChar.X(), challengerChar.Y());
                defender.GetCharController().CharaVictory(defList[i]);
                challenger.GetCharController().CharaLose(challengerChar);
            }
        }
        // 戦後アニメーション
        _isBattle = BATTLE_STATE.Finished;
        yield return null;
    }

    public Compatibility CheckCompatibility(ICharacter.TYPE challenger, ICharacter.TYPE defender)
    {
        int result = (challenger - defender + 3) % 3;
        return (Compatibility)result;
    }

    public int GetDistance(ICharacter c1, ICharacter c2)
    {
        return GetDistance(c1.X(), c1.Y(), c2.X(), c2.Y());
    }

    public int GetDistance(int x1, int y1, int x2, int y2)
    {
        int x = Mathf.Abs(x1 - x2);
        int y = Mathf.Abs(y1 - y2);
        return x + y;
    }
}
