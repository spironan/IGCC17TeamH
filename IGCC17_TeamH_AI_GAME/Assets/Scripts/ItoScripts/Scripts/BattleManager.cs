using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{

    enum Compatibility
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
    public bool Battle(IPlayer challenger, IPlayer defender)
    {
        // 攻撃側の周囲を探索
        // 勝てるキャラクターと負けるキャラクターを探す
        // それぞれの状態を変更する

        ICharacter challengerChara = challenger.GetCharController().GetCurrentCharacter();
        foreach (ICharacter def in defender.GetCharController().GetCharacters())
        {
            if (def.GetMyState() == ICharacter.STATE.FROZEN) continue;
            if (GetDistance(challengerChara, def) > challengerChara.GetAttackRange()) continue;
            if (challengerChara.GetMyType() == def.GetMyType()) continue;
            if (CheckCompatibility(challengerChara.GetMyType(), def.GetMyType()) == Compatibility.Strong)
            {
                challengerChara.Victory();
                def.Defeated();
            }
            else
            {
                if (def.GetMyState() == ICharacter.STATE.GREEN) continue;
                def.Victory();
                challengerChara.Defeated();
            }
        }
        return true;
    }

    Compatibility CheckCompatibility(ICharacter.TYPE challenger, ICharacter.TYPE defender)
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
