using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagniAttackIntend : EnemyIntend
{
    [Header("基础攻击值1dx, 只需填写x")]
    public int dmgBase;

    public override GameAction GetGameAction(EnemyView enemy)
    {
        MagnifyAttackGA ga = new(new() {HeroSystem.Instance.HeroView}, enemy, dmgBase);
        return ga;
    }
}
