using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagniAttackIntend : EnemyIntend, IHaveDmgInfo
{
    [Header("基础攻击值1dx, 只需填写x")]
    public int dmgBase;

    string dmgInfo;

    string IHaveDmgInfo.dmgStrInfo { 
        get => dmgInfo; 
        set => dmgInfo = value; 
    }

    public override GameAction GetGameAction(EnemyView enemy)
    {
        MagnifyAttackGA ga = new(new() {HeroSystem.Instance.HeroView}, enemy, dmgBase);
        dmgInfo = ga.dmgStrInfo;
        return ga;
    }
}
