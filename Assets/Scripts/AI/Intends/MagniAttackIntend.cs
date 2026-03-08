using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagniAttackIntend : EnemyIntend, IHaveDmgInfo
{
    [Header("基础攻击值1dx, 只需填写x")]
    public int dmgBase;

    public string GetDmgInfo(EnemyView enemyView)
    {
        int muti = enemyView.M.GetStatusEffectStacks(StatusEffectType.MUTIATK);
        return muti + "d" + dmgBase;
    }

    public override GameAction GetGameAction(EnemyView enemy)
    {
        MagnifyAttackGA ga = new(new() {HeroSystem.Instance.HeroView}, enemy, GetDmgInfo(enemy));
        return ga;
    }
}
