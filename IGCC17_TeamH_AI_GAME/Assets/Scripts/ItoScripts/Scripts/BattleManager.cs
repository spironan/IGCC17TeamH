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

    }


    /// <summary>
    /// won returned true
    /// </summary>
    /// <param name="challenger"></param>
    /// <param name="defender"></param>
    /// <returns></returns>
    public bool Battle(IPlayer challenger, IPlayer defender, BoardController board)
    {
        // 攻撃側の周囲を探索
        // 勝てるキャラクターと負けるキャラクターを探す
        // それぞれの状態を変更する
        ICharacter challengerChara = challenger.GetCharController().GetCurrentCharacter();
        List<ICharacter> defList = defender.GetCharController().GetCharacters();
        for (int i = defList.Count - 1; i >= 0; --i)
        {
            if (defList[i].GetMyState() == ICharacter.STATE.FROZEN) continue;
            if (GetDistance(challengerChara, defList[i]) > challengerChara.GetAttackRange()) continue;
            if (challengerChara.GetMyType() == defList[i].GetMyType()) continue;
            if (CheckCompatibility(challengerChara.GetMyType(), defList[i].GetMyType()) == Compatibility.Strong)
            {
                if (challengerChara.GetMyState() == ICharacter.STATE.GREEN) continue;
                board.AddObstacle(defList[i].X(), defList[i].Y());
                challenger.GetCharController().CharaVictory(challengerChara);
                defender.GetCharController().CharaLose(defList[i]);
                
            }
            else
            {
                if (defList[i].GetMyState() == ICharacter.STATE.GREEN) continue;
                board.AddObstacle(challengerChara.X(), challengerChara.Y());
                defender.GetCharController().CharaVictory(defList[i]);
                challenger.GetCharController().CharaLose(challengerChara);
                
            }
        }
        
        return true;
    }

    public Compatibility CheckCompatibility(ICharacter.TYPE challenger, ICharacter.TYPE defender)
    {
        int result = (challenger - defender + 3) % 3;
        return (Compatibility)result;
    }

    int GetDistance(ICharacter c1, ICharacter c2)
    {
        int x = Mathf.Abs(c1.X() - c2.X());
        int y = Mathf.Abs(c1.Y() - c2.Y());
        return x + y;
    }
}
